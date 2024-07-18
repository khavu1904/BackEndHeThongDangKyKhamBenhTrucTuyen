using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebDangKyKhamChuaBenh.Data;
using WebDangKyKhamChuaBenh.Models;
using System.Security.Cryptography;
using System.Text;
using System.Net.Mail;

namespace WebDangKyKhamChuaBenh.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BenhNhanController : ControllerBase
    {
        private readonly MyAppDbContext _context;

        public BenhNhanController(MyAppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BenhNhan>>> GetBenhNhan() //Xem tất cả thông tin nhóm quyền
        {
            return await _context.BenhNhan!.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BenhNhan>> GetIdBenhNhan(int id) //Tìm kiếm nhóm quyền theo id
        {
            var benhnhan = await _context.BenhNhan!.FindAsync(id);

            if (benhnhan == null)
            {
                return NotFound();
            }

            return benhnhan;
        }
        [HttpPost]
        public async Task<ActionResult> PostBenhNhan(BenhNhan benhnhan) // Thêm mới bệnh nhân
        {
            if (benhnhan == null)
            {
                return BadRequest();
            }

            // Kiểm tra sđt theo nhà mạng Việt Nam
            string phonePattern = @"^(03|05|07|08|09)\d{8}$";
            if (!Regex.IsMatch(benhnhan.SDT, phonePattern))
            {
                return BadRequest("Số điện thoại không hợp lệ. Số điện thoại phải theo định dạng của nhà mạng Việt Nam.");
            }

            // Kiểm tra định dạng email
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(benhnhan.Email, emailPattern))
            {
                return BadRequest("Địa chỉ email không hợp lệ.");
            }

            // Kiểm tra xem số điện thoại đã được đăng kí chưa
            bool phoneExists = await _context.BenhNhan.AnyAsync(bn => bn.SDT == benhnhan.SDT);
            if (phoneExists)
            {
                return Conflict("Số điện thoại này đã được đăng ký.");
            }

            // kiểm tra xem email đã đăng ký chưa
            bool emailExists = await _context.BenhNhan.AnyAsync(bn => bn.Email == benhnhan.Email);
            if (emailExists)
            {
                return Conflict("Địa chỉ email này đã được đăng ký.");
            }

            // Kiểm tra ngày sinh phải lớn hơn 18 tuổi
            if (benhnhan.NgaySinh.HasValue)
            {
                int age = DateTime.Now.Year - benhnhan.NgaySinh.Value.Year;
                if (DateTime.Now.DayOfYear < benhnhan.NgaySinh.Value.DayOfYear)
                {
                    age--;
                }

                if (age < 18)
                {
                    return BadRequest("Bệnh nhân phải lớn hơn 18 tuổi.");
                }
            }
            else
            {
                benhnhan.NgaySinh = DateTime.Now; // Default to the current date if not provided
            }

            try
            {
                // Băm mật khẩu trước khi lưu vào cở sở dữ liệu
                string hashedPassword = HashPassword(benhnhan.MatKhau);

                // Tạo một bản ghi mới
                BenhNhan newBenhNhan = new BenhNhan
                {
                    HoTenBN = benhnhan.HoTenBN ?? "Chưa có", // Sử dụng toán tử null coalescing để thiết lập giá trị mặc định
                    DiaChi = benhnhan.DiaChi ?? "Chưa có",
                    CMND_CCCD = benhnhan.CMND_CCCD ?? "Chưa có",
                    SDT = benhnhan.SDT,
                    NgaySinh = benhnhan.NgaySinh ?? DateTime.Now, // Giả sử NgaySinh là kiểu DateTime
                    GioiTinh = benhnhan.GioiTinh ?? "Chưa có",
                    MaBHYT = benhnhan.MaBHYT ?? "Chưa có",
                    DanToc = benhnhan.DanToc ?? "Chưa có",
                    NgheNghiep = benhnhan.NgheNghiep ?? "Chưa có",
                    Email = benhnhan.Email,
                    AnhDaiDien = benhnhan.AnhDaiDien ?? "Chưa có",
                    MatKhau = hashedPassword, // Store the hashed password
                    MaQuyen = benhnhan.MaQuyen // MaQuyen sẽ được thiết lập bởi logic phía client
                };

                _context.BenhNhan.Add(newBenhNhan);
                await _context.SaveChangesAsync();

                return CreatedAtAction(
                    nameof(GetBenhNhan),
                    new { id = newBenhNhan.MaBN, Message = "Thêm mới bệnh nhân thành công" }
                );
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the record.");
            }
        }

        // Function to hash the password
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBenhNhan(int id) //Xóa bệnh nhân theo id
        {
            try
            {
                var benhnhan = await _context.BenhNhan.FindAsync(id);
                if (benhnhan == null)
                {
                    return NotFound();
                }

                _context.BenhNhan.Remove(benhnhan);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the record.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBenhNhan(int id, BenhNhan benhnhan) //Cập nhật thông tin bệnh nhân
        {
            if (id != benhnhan.MaBN)
            {
                return BadRequest();
            }

            // Kiểm tra số điện thoại
            if (!IsValidPhoneNumber(benhnhan.SDT))
            {
                return BadRequest("Số điện thoại không hợp lệ.");
            }

            // Kiểm tra độ dài CCCD/CMND
            if (benhnhan.CMND_CCCD.Length != 12)
            {
                return BadRequest("CCCD/CMND phải có 12 ký tự.");
            }

            // Kiểm tra email hợp lệ
            if (!IsValidEmail(benhnhan.Email))
            {
                return BadRequest("Địa chỉ email không hợp lệ.");
            }

            // Kiểm tra ngày sinh phải lớn hơn 16 tuổi
            if (benhnhan.NgaySinh == null || CalculateAge(benhnhan.NgaySinh.Value) < 14)
            {
                return BadRequest("Bệnh nhân phải lớn hơn 16 tuổi.");
            }

            // Kiểm tra xem số điện thoại có tồn tại không
            var existingPhone = await _context.BenhNhan.AnyAsync(bn => bn.SDT == benhnhan.SDT && bn.MaBN != id);
            if (existingPhone)
            {
                return BadRequest("Số điện thoại đã tồn tại.");
            }

            // Kiểm tra xem email có tồn tại không
            var existingEmail = await _context.BenhNhan.AnyAsync(bn => bn.Email == benhnhan.Email && bn.MaBN != id);
            if (existingEmail)
            {
                return BadRequest("Địa chỉ email đã tồn tại.");
            }

            try
            {
                var existingBenhNhan = await _context.BenhNhan.FindAsync(id);
                if (existingBenhNhan == null)
                {
                    return NotFound();
                }

                _context.Entry(existingBenhNhan).CurrentValues.SetValues(benhnhan);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BenhNhanExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the record.");
            }
        }
        [HttpPut("UpdatePassword/{id}")]
        public async Task<IActionResult> UpdatePassword(int id, [FromBody] string newPassword)
        {
            try
            {
                if (string.IsNullOrEmpty(newPassword))
                {
                    return BadRequest("New password is required.");
                }

                // Perform validation on the new password, e.g., minimum length, special characters requirement, etc.
                // You can add your validation logic here.

                string hashedPassword = HashPassword(newPassword);

                var benhNhan = await _context.BenhNhan.FindAsync(id);
                if (benhNhan == null)
                {
                    return NotFound();
                }

                benhNhan.MatKhau = hashedPassword; // Update the password

                await _context.SaveChangesAsync();

                return Ok("Password updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the password: {ex.Message}");
            }
        }

        private bool BenhNhanExists(int id)
        {
            return _context.BenhNhan.Any(e => e.MaBN == id);
        }

        // Kiểm tra định dạng email hợp lệ
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        // Kiểm tra số điện thoại hợp lệ
        private bool IsValidPhoneNumber(string phoneNumber)
        {
            // Kiểm tra độ dài
            if (phoneNumber.Length != 10)
            {
                return false;
            }

            // Kiểm tra 3 số đầu theo nhà mạng Việt Nam
            var validPrefixes = new List<string> { "032", "033", "034", "035", "036", "037", "038", "039",
                                                   "070", "076", "077", "078", "079",
                                                   "081", "082", "083", "084", "085", "086",
                                                   "088", "089",
                                                   "090", "093", "094", "096", "097", "098", "099" };
            return validPrefixes.Any(prefix => phoneNumber.StartsWith(prefix));
        }
        // Tính tuổi dựa trên ngày sinh
        private int CalculateAge(DateTime birthDate)
        {
            var today = DateTime.Today;
            var age = today.Year - birthDate.Year;
            if (birthDate.Date > today.AddYears(-age)) age--;
            return age;
        }
    }
}
