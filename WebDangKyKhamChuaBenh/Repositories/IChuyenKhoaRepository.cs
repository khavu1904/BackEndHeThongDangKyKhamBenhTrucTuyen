using WebDangKyKhamChuaBenh.Models;

namespace WebDangKyKhamChuaBenh.Repositories
{
    public interface IChuyenKhoaRepository
    {
        Task<List<ChuyenKhoa>> GetAllChuyenKhoasAsync();
        Task<ChuyenKhoa> GetChuyenKhoaAsync(int id);
        Task<int> AddChuyenKhoaAsync(ChuyenKhoa model);
        Task UpdateChuyenKhoaAsync(int id, ChuyenKhoa model);
        Task DeleteChuyenKhoaAsync(int id);
    }
}
