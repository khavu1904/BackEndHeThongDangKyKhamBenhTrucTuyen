using WebDangKyKhamChuaBenh.Models;

namespace WebDangKyKhamChuaBenh.Repositories
{
    public interface ILichLamViecRepository
    {
        public Task<List<LichLamViec>> GetAllLichLamViecsAsync();
        public Task<LichLamViec> GetLichLamViecAsync(int id);
        public Task<int> AddLichLamViecAsync(LichLamViec model);
        public Task UpdateLichLamViecAsync(int id, LichLamViec model);
        public Task DeleteLichLamViecAsync(int id);
        Task<List<LichLamViec>> GetLichLamViecByMaNVAsync(int maNV);

    }
}
