using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebDangKyKhamChuaBenh.Data;
using WebDangKyKhamChuaBenh.DTO;
using WebDangKyKhamChuaBenh.Models;

namespace WebDangKyKhamChuaBenh.Repositories
{
    public class NhomQuyenRepository : INhomQuyenRepository
    {
        private readonly MyAppDbContext _context;
        private readonly IMapper _mapper;

        public NhomQuyenRepository(MyAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> AddNhomQuyenAsync(NhomQuyen model) //Thêm mới quyền 
        {
            var newNhomQuyen = _mapper.Map<NhomQuyen>(model);
            _context.NhomQuyen!.Add(newNhomQuyen);
            await _context.SaveChangesAsync();

            return newNhomQuyen.MaQuyen;
        }

        public async Task<List<NhomQuyen>> GetAllNhomQuyensAsync()//Lấy tất cả thông tin sách
        {
            var nhomquyens = await _context.NhomQuyen!.ToListAsync();
            return _mapper.Map<List<NhomQuyen>>(nhomquyens);
        }

        public async Task<NhomQuyen> GetNhomQuyenAsync(int id)//Tìm nhóm quyền theo id
        {
            var nhomquyen = await _context.NhomQuyen!.FindAsync(id);
            return _mapper.Map<NhomQuyen>(nhomquyen);
        }

        public async Task UpdateNhomQuyenAsync(int id, NhomQuyen model)//Cập nhật thông nhóm quyền
        {
            if (id == model.MaQuyen)
            {
                var updateBook = _mapper.Map<NhomQuyen>(model);
                _context.NhomQuyen!.Update(updateBook);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteNhomQuyenAsync(int id)//Xóa nhóm quyền
        {
            var deleteNhomQuyen = _context.NhomQuyen!.SingleOrDefault(b => b.MaQuyen == id);
            if (deleteNhomQuyen != null)
            {
                _context.NhomQuyen!.Remove(deleteNhomQuyen);
                await _context.SaveChangesAsync();
            }
        }
    }
}
