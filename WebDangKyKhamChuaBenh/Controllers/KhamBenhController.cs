using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata;
using WebDangKyKhamChuaBenh.Data;
using WebDangKyKhamChuaBenh.Models;
using WebDangKyKhamChuaBenh.Repositories;

namespace WebDangKyKhamChuaBenh.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KhamBenhController : ControllerBase
    {
        private readonly MyAppDbContext _context;
        private readonly IKhamBenhRepository _khamBenhRepository;

        public KhamBenhController(MyAppDbContext context, IKhamBenhRepository khamBenhRepository)
        {
            _context = context;
            _khamBenhRepository = khamBenhRepository;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<KhamBenh>> GetKhamBenhId(int id) //Tìm kiếm nhóm quyền theo id
        {
            var khambenh = await _context.KhamBenh!.FindAsync(id);

            if (khambenh == null)
            {
                return NotFound();
            }

            return khambenh;
        }
        [HttpPost]
        public async Task<IActionResult> RegisterKhamBenh([FromBody] KhamBenh khamBenh)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var token = Request.Headers["Authorization"].ToString().Split(" ").Last();
            var jwtToken = new JwtSecurityTokenHandler().ReadToken(token) as JwtSecurityToken;
            var maBN = jwtToken?.Claims.First(claim => claim.Type == "MaBN").Value;

            if (string.IsNullOrEmpty(maBN))
            {
                return Unauthorized();
            }

            khamBenh.MaBN = int.Parse(maBN);
            var createdKhamBenh = await _khamBenhRepository.AddKhamBenhAsync(khamBenh);

            return Ok(createdKhamBenh);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<KhamBenh>>> GetAllKhamBenh() //Xem tất cả thông tin nhóm quyền
        {
            return await _context.KhamBenh!.ToListAsync();
        }
        [HttpPut("UpdateKhamBenhStatusAsync/{id}")]
        public async Task<IActionResult> UpdateKhamBenhStatusAsync(int id, string newStatus, string tenNhanVienDuyet)
        {
            try
            {
                var khamBenh = await _context.KhamBenh.FirstOrDefaultAsync(kb => kb.MaKhamBenh == id);

                if (khamBenh == null)
                {
                    return NotFound("KhamBenh not found");
                }

                // Cập nhật trạng thái mới
                khamBenh.TrangThai = newStatus;
                khamBenh.TenNhanVienDuyet = tenNhanVienDuyet;

                _context.KhamBenh.Update(khamBenh);
                await _context.SaveChangesAsync();

                return Ok("KhamBenh status updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("maBN/{maBN}")]
        public async Task<ActionResult<List<KhamBenhDetail>>> GetKhamBenhDetails(int maBN)
        {
            try
            {
                var khamBenhDetails = await _khamBenhRepository.GetKhamBenhDetailsByMaBNAsync(maBN);
                return Ok(khamBenhDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("by-nhanvienduyet")]
        public async Task<IActionResult> GetKhamBenhByNhanVienDuyet([FromQuery] string tenNhanVienDuyet)
        {
            var result = await _khamBenhRepository.GetKhamBenhByTenNhanVienDuyetAsync(tenNhanVienDuyet);
            if (result == null || !result.Any())
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
