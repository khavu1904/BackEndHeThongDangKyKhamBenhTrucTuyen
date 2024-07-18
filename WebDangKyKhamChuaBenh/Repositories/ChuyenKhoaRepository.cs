using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebDangKyKhamChuaBenh.Data;
using WebDangKyKhamChuaBenh.Models;

namespace WebDangKyKhamChuaBenh.Repositories
{
    public class ChuyenKhoaRepository : IChuyenKhoaRepository
    {
        private readonly MyAppDbContext _context;
        private readonly IMapper _mapper;

        public ChuyenKhoaRepository(MyAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> AddChuyenKhoaAsync(ChuyenKhoa model) //Thêm mới chuyên khoa
        {
            var newChuyenKhoa = _mapper.Map<ChuyenKhoa>(model);
            _context.ChuyenKhoa!.Add(newChuyenKhoa);
            await _context.SaveChangesAsync();

            return newChuyenKhoa.MaChuyenKhoa;
        }

        Task IChuyenKhoaRepository.DeleteChuyenKhoaAsync(int id) //Xóa chuyên khoa theo id
        {
            throw new NotImplementedException();
        }

        public async Task<List<ChuyenKhoa>> GetAllChuyenKhoasAsync() //Lấy tất cả thông tin chuyên khoa
        {
            var chuyenkhoas = await _context.ChuyenKhoa!.ToListAsync();
            return _mapper.Map<List<ChuyenKhoa>>(chuyenkhoas);
        }

        Task<ChuyenKhoa> IChuyenKhoaRepository.GetChuyenKhoaAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task IChuyenKhoaRepository.UpdateChuyenKhoaAsync(int id, ChuyenKhoa model)
        {
            throw new NotImplementedException();
        }
    }
}
