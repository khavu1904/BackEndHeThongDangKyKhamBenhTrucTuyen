using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebDangKyKhamChuaBenh.Data;
using WebDangKyKhamChuaBenh.Models;

namespace WebDangKyKhamChuaBenh.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChuyenKhoaController : ControllerBase
    {
        private readonly MyAppDbContext _context;

        public ChuyenKhoaController(MyAppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChuyenKhoa>>> GetChuyenKhoa() //Xem tất cả thông tin bệnh nhân
        {
            return await _context.ChuyenKhoa!.ToListAsync();
        }


        [HttpPost]
        public async Task<ActionResult> PostChuyenKhoa(ChuyenKhoa chuyenkhoa) //Thêm mới bệnh nhân
        {
            try
            {
                _context.ChuyenKhoa!.Add(chuyenkhoa);
                await _context.SaveChangesAsync();

                return CreatedAtAction(
                    nameof(GetChuyenKhoa),
                    new { Message = "Thêm mới chuyên khoa thành công" }
                );
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the record.");
            }
        }

    }
}
