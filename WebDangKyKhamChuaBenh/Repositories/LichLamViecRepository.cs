using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebDangKyKhamChuaBenh.Data;
using WebDangKyKhamChuaBenh.Models;

namespace WebDangKyKhamChuaBenh.Repositories
{
    public class LichLamViecRepository : ILichLamViecRepository
    {
        private readonly MyAppDbContext _context;
        private readonly IMapper _mapper;

        public LichLamViecRepository(MyAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        Task ILichLamViecRepository.DeleteLichLamViecAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<LichLamViec>> GetAllLichLamViecsAsync()
        {
            return await _context.LichLamViec.ToListAsync();
        }

        public async Task<LichLamViec> GetLichLamViecAsync(int id)
        {
            var lichlamviec = await _context.LichLamViec!.FindAsync(id);
            return _mapper.Map<LichLamViec>(lichlamviec);
        }
        public async Task<List<LichLamViec>> GetLichLamViecByMaNVAsync(int maNV)
        {
            return await _context.LichLamViec
                .Where(l => l.MaNV == maNV)
                .ToListAsync();
        }

        public async Task<int> AddLichLamViecAsync(LichLamViec model)
        {
            var newLichLamViec = _mapper.Map<LichLamViec>(model);
            _context.LichLamViec!.Add(newLichLamViec);
            await _context.SaveChangesAsync();

            return newLichLamViec.MaLichLamViec;
        }

        public Task UpdateLichLamViecAsync(int id, LichLamViec model)
        {
            throw new NotImplementedException();
        }
    }
}
