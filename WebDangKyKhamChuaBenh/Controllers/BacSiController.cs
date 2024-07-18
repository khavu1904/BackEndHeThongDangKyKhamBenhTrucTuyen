using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebDangKyKhamChuaBenh.Data;
using WebDangKyKhamChuaBenh.DTO;
using WebDangKyKhamChuaBenh.Repositories;

namespace WebDangKyKhamChuaBenh.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BacSiController : ControllerBase
    {
        private readonly MyAppDbContext _context;
        private readonly IBacSiRepository _bacSiRepository; // Khai báo repository

        public BacSiController(MyAppDbContext context, IBacSiRepository bacSiRepository)
        {
            _context = context;
            _bacSiRepository = bacSiRepository;
        }
        [HttpGet("with-chuyenkhoa")]
        public async Task<ActionResult<IEnumerable<BacSiInfoDto>>> GetBacSiInfo()
        {
            var bacSiInfo = await _bacSiRepository.GetBacSiWithChuyenKhoa();
            return Ok(bacSiInfo);
        }
        [HttpGet("with-chuyenkhoa-ma/{id}")]
        public async Task<ActionResult<IEnumerable<BacSiInfoDto>>> GetBacSiInfo(int id)
        {
            var bacSiInfo = await _bacSiRepository.GetBacSiWithChuyenKhoaId(id);
            return Ok(bacSiInfo);
        }
        [HttpGet("{maNV}")]
        public async Task<ActionResult<BacSiChuyenKhoaDto>> GetBacSiDetails(int maNV)
        {
            var bacSiDetails = await _bacSiRepository.GetBacSiChuyenKhoa(maNV);
            if (bacSiDetails == null)
            {
                return NotFound();
            }

            return Ok(bacSiDetails.Value); // Trả về phần value của bacSiDetails
        }

    }
}
