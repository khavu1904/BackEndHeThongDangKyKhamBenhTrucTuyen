using Microsoft.AspNetCore.Authorization;
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
    public class NhomQuyenController : ControllerBase
    {
        private readonly MyAppDbContext _context;

        public NhomQuyenController(MyAppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NhomQuyen>>> GetNhomQuyen() //Xem tất cả thông tin nhóm quyền
        {
            return await _context.NhomQuyen!.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NhomQuyen>> GetNhomQuyen(int id) //Tìm kiếm nhóm quyền theo id
        {
            var book = await _context.NhomQuyen!.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        [HttpPost]
        public async Task<ActionResult> PostNhomQuyen(NhomQuyen nhomquyen) //Thêm mới nhóm quyền
        {
            try
            {
                _context.NhomQuyen!.Add(nhomquyen);
                await _context.SaveChangesAsync();

                return CreatedAtAction(
                    nameof(GetNhomQuyen),
                    new { Message = "Thêm mới quyền thành công"}
                );
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the record.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNhomQuyen(int id) //Xóa nhóm quyền theo id
        {
            var nhomquyen = await _context.NhomQuyen!.FindAsync(id);
            if (nhomquyen == null)
            {
                return NotFound();
            }

            _context.NhomQuyen.Remove(nhomquyen);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutNhomQuyen(int id, NhomQuyen nhomquyen) //Cập nhật lại nhóm quyền
        {
            if (id != nhomquyen.MaQuyen)
            {
                return BadRequest();
            }

            _context.Entry(nhomquyen).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NhomQuyenExists(id))
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
        private bool NhomQuyenExists(int id)
        {
            return _context.NhomQuyen!.Any(e => e.MaQuyen == id);
        }
    }
}
