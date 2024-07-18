using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebDangKyKhamChuaBenh.Data;
using WebDangKyKhamChuaBenh.Models;

namespace WebDangKyKhamChuaBenh.Repositories
{
    public class NhanVienRepository : INhanVienRepository
    {
        private readonly MyAppDbContext _context;
        private readonly IMapper _mapper;

        public NhanVienRepository(MyAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> AddNhanVienAsync(NhanVien model) //Thêm mới nhân viên
        {
            var newNhanVien = _mapper.Map<NhanVien>(model);
            _context.NhanVien!.Add(newNhanVien);
            await _context.SaveChangesAsync();

            return newNhanVien.MaNV;
        }

        public async Task DeleteNhanVienAsync(int id) //Xóa nhân viên
        {
            var deleteNhanVien = _context.NhanVien!.SingleOrDefault(b => b.MaNV == id);
            if (deleteNhanVien != null)
            {
                _context.NhanVien!.Remove(deleteNhanVien);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<NhanVien>> GetAllNhanViensAsync() //Xem tất cả thông tin nhân viên
        {
            var nhanviens = await _context.NhanVien!.ToListAsync();
            return _mapper.Map<List<NhanVien>>(nhanviens);
        }

        public async Task<NhanVien> GetNhanVienAsync(int id) //Tìm kiếm nhân viên theo id
        {
            var nhanvien = await _context.NhanVien!.FindAsync(id);
            return _mapper.Map<NhanVien>(nhanvien);
        }

        public async Task UpdateNhanVienPasswordAsync(int id, string newPassword)
        {
            var nhanvien = await _context.NhanVien.FindAsync(id);
            if (nhanvien != null)
            {
                nhanvien.MatKhau = newPassword;
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateNhanVienAsync(int id, NhanVien model) //Cập nhật thông tin nhân viên
        {
            if (id == model.MaNV)
            {
                var updateNhanVien = _mapper.Map<NhanVien>(model);
                _context.NhanVien!.Update(updateNhanVien);
                await _context.SaveChangesAsync();
            }
        }
    }
}
