using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebDangKyKhamChuaBenh.Data;
using WebDangKyKhamChuaBenh.Models;

namespace WebDangKyKhamChuaBenh.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhongKhamController : ControllerBase
    {
        private readonly MyAppDbContext _context;

        public PhongKhamController(MyAppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhongKham>>> GetAllPhongKham() //Xem tất cả thông tin nhóm quyền
        {
            return await _context.PhongKham!.ToListAsync();
        }
    }
}
