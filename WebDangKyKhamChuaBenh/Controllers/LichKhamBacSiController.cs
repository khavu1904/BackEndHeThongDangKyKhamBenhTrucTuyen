using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebDangKyKhamChuaBenh.Data;
using WebDangKyKhamChuaBenh.DTO;
using WebDangKyKhamChuaBenh.Repositories;

namespace WebDangKyKhamChuaBenh.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LichKhamBacSiController : ControllerBase
    {
        private readonly MyAppDbContext _context;
        private readonly ILichKhamBacSiRepository _lichkhambacSiRepository; // Khai báo repository

        public LichKhamBacSiController(MyAppDbContext context, ILichKhamBacSiRepository lichkhambacSiRepository)
        {
            _context = context;
            _lichkhambacSiRepository = lichkhambacSiRepository;
        }
        [HttpGet("kham-benh/{maNV}")]
        public async Task<ActionResult<List<LichKhamBacSiDto>>> GetKhamBenhByMaNV(int maNV)
        {
            var result = await _lichkhambacSiRepository.GetKhamBenhByMaNV(maNV);
            return Ok(result);
        }
    }
}
