using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using WebDangKyKhamChuaBenh.Data;
using WebDangKyKhamChuaBenh.Models;

namespace WebDangKyKhamChuaBenh.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NhanVienController : ControllerBase
    {
        private readonly MyAppDbContext _context;

        public NhanVienController(MyAppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NhanVien>>> GetAllNhanVien() //Xem tất cả thông tin nhân viên
        {
            return await _context.NhanVien!.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NhanVien>> GetNhanVien(int id) //Tìm kiếm nhóm quyền theo id
        {
            var nhanvien = await _context.NhanVien!.FindAsync(id);

            if (nhanvien == null)
            {
                return NotFound();
            }

            return nhanvien;
        }

        [HttpPost]
        public async Task<ActionResult> PostNhanVien (NhanVien benhnhan) //Thêm mới nhân viên
        {
            try
            {
                _context.NhanVien!.Add(benhnhan);
                await _context.SaveChangesAsync();

                return CreatedAtAction(
                    nameof(GetNhanVien),
                    new { Message = "Thêm mới nhân viên thành công" }
                );
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the record.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNhanVien(int id) //Xóa nhân viên theo id
        {
            var nhanvien = await _context.NhanVien.FindAsync(id);
            if (nhanvien == null)
            {
                return NotFound();
            }

            _context.NhanVien.Remove(nhanvien);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutNhanVien(int id, NhanVien nhanvien) //Cập nhật lại nhân viên
        {
            if (id != nhanvien.MaNV)
            {
                return BadRequest();
            }

            _context.Entry(nhanvien).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NhanVienExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        //
        [HttpPut("UpdatePassword/{id}")]
        public async Task<IActionResult> UpdatePassword(int id, [FromBody] string newPassword)
        {
            try
            {
                if (string.IsNullOrEmpty(newPassword))
                {
                    return BadRequest("Cần mật khẩu mới.");
                }
                string hashedPassword = HashPassword(newPassword);

                var nhanvien = await _context.NhanVien.FindAsync(id);
                if (nhanvien == null)
                {
                    return NotFound();
                }

                nhanvien.MatKhau = hashedPassword; // Update the password

                await _context.SaveChangesAsync();

                return Ok("Đã cập nhật thành công mật khẩu.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Đã xảy ra lỗi khi cập nhật mật khẩu: {ex.Message}");
            }
        }
        //
        private bool NhanVienExists(int id)
        {
            return _context.NhanVien!.Any(e => e.MaNV == id);
        }
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
    }
}
