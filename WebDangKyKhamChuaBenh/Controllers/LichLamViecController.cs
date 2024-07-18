using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebDangKyKhamChuaBenh.Data;
using WebDangKyKhamChuaBenh.Models;
using WebDangKyKhamChuaBenh.Repositories;

namespace WebDangKyKhamChuaBenh.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LichLamViecController : ControllerBase
    {
        private readonly MyAppDbContext _context;
        private readonly ILichLamViecRepository _lichlamviecRepository;

        public LichLamViecController(MyAppDbContext context, ILichLamViecRepository lichlamviecRepository)
        {
            _context = context;
            _lichlamviecRepository = lichlamviecRepository; ;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LichLamViec>>> GetLichLamViec() //Xem tất cả thông tin nhóm quyền
        {
            return await _context.LichLamViec!.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<LichLamViec>> GetLichLamViec(int id) //Tìm kiếm nhóm quyền theo id
        {
            var lichlamviec = await _context.LichLamViec!.FindAsync(id);

            if (lichlamviec == null)
            {
                return NotFound();
            }

            return lichlamviec;
        }
        [HttpGet("nhanvien/{maNV}")]
        public async Task<ActionResult<IEnumerable<LichLamViec>>> GetLichLamViecByMaNV(int maNV)
        {
            var lichLamViecs = await _lichlamviecRepository.GetLichLamViecByMaNVAsync(maNV);
            return Ok(lichLamViecs);
        }
        [HttpPost]
        public async Task<ActionResult> PostLichLamViec(LichLamViec lichLamViec) // Thêm mới lịch làm việc
        {
            try
            {
                DateTime currentDate = DateTime.Now.Date; // Lấy ngày hiện tại (không bao gồm thời gian)

                // Kiểm tra nếu NgayTao nhỏ hơn ngày hiện tại
                if (lichLamViec.NgayTao < currentDate)
                {
                    return BadRequest("Ngày tạo không được nhỏ hơn ngày hiện tại.");
                }

                // Kiểm tra nếu NgayTao không được bằng hoặc lớn hơn NgayBatDau
                if (lichLamViec.NgayTao >= lichLamViec.NgayBatDau)
                {
                    return BadRequest("Ngày tạo không được bằng hoặc lớn hơn ngày bắt đầu.");
                }

                // Kiểm tra xung đột về ca và ngày bắt đầu với mã phòng khám
                bool isConflict = await _context.LichLamViec!.AnyAsync(llv =>
                    llv.NgayBatDau == lichLamViec.NgayBatDau &&
                    llv.Ca == lichLamViec.Ca &&
                    llv.MaPhongKham == lichLamViec.MaPhongKham
                );

                if (isConflict)
                {
                    return Conflict("Ca và ngày này đã được đăng ký tại phòng khám này. Vui lòng chọn ngày, ca, hoặc phòng khám khác.");
                }

                // Nếu không có xung đột, thêm lịch làm việc mới
                _context.LichLamViec!.Add(lichLamViec);
                await _context.SaveChangesAsync();

                return CreatedAtAction(
                    nameof(GetLichLamViec),
                    new { id = lichLamViec.MaLichLamViec },
                    new { Message = "Thêm mới lịch làm việc thành công" }
                );
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the record.");
            }
        }
    }
}
