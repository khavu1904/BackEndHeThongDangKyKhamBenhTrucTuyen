using WebDangKyKhamChuaBenh.Models;

namespace WebDangKyKhamChuaBenh.Repositories
{
    public interface INhanVienRepository
    {
        Task<List<NhanVien>> GetAllNhanViensAsync();
        Task<NhanVien> GetNhanVienAsync(int id);
        Task<int> AddNhanVienAsync(NhanVien model);
        Task UpdateNhanVienAsync(int id, NhanVien model);
        Task DeleteNhanVienAsync(int id);
        Task UpdateNhanVienPasswordAsync(int id, string newPassword);
    }
}
