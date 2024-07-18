using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WebDangKyKhamChuaBenh.Data;
using WebDangKyKhamChuaBenh.Models;

namespace WebDangKyKhamChuaBenh.Repositories
{
    public class BenhNhanRepository : IBenhNhanRepository
    {
        private readonly MyAppDbContext _context;
        private readonly IMapper _mapper;

        public BenhNhanRepository(MyAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> AddBenhNhanAsync(BenhNhan model) // Thêm mới bệnh nhân
        {
            var newBenhNhan = _mapper.Map<BenhNhan>(model);
            _context.BenhNhan.Add(newBenhNhan);
            await _context.SaveChangesAsync();

            return newBenhNhan.MaBN;
        }

        public async Task DeleteBenhNhanAsync(int id) // Xóa bệnh nhân
        {
            var deleteBenhNhan = await _context.BenhNhan.FindAsync(id);
            if (deleteBenhNhan != null)
            {
                _context.BenhNhan.Remove(deleteBenhNhan);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<BenhNhan>> GetAllBenhNhanAsync() // Lấy tất cả thông tin bệnh nhân
        {
            var benhNhanList = await _context.BenhNhan.ToListAsync();
            return _mapper.Map<List<BenhNhan>>(benhNhanList);
        }

        public async Task<BenhNhan> GetBenhNhanAsync(int id) // Tìm kiếm bệnh nhân theo id
        {
            var benhNhan = await _context.BenhNhan.FindAsync(id);
            return _mapper.Map<BenhNhan>(benhNhan);
        }

        public async Task UpdateBenhNhanAsync(int id, BenhNhan model) // Cập nhật thông tin bệnh nhân
        {
            if (id == model.MaBN)
            {
                var updateBenhNhan = _mapper.Map<BenhNhan>(model);
                _context.Entry(updateBenhNhan).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateBenhNhanPasswordAsync(int id, string newPassword)// Cập nhật lại mật khẩu
        {
            var benhNhan = await _context.BenhNhan.FindAsync(id);
            if (benhNhan != null)
            {
                benhNhan.MatKhau = newPassword;
                await _context.SaveChangesAsync();
            }
        }
    }
}
