using System.Collections.Generic;
using System.Threading.Tasks;
using WebDangKyKhamChuaBenh.Models;

namespace WebDangKyKhamChuaBenh.Repositories
{
    public interface IBenhNhanRepository
    {
        Task<List<BenhNhan>> GetAllBenhNhanAsync();
        Task<BenhNhan> GetBenhNhanAsync(int id);
        Task<int> AddBenhNhanAsync(BenhNhan model);
        Task UpdateBenhNhanAsync(int id, BenhNhan model);
        Task DeleteBenhNhanAsync(int id);
        Task UpdateBenhNhanPasswordAsync(int id, string newPassword);
    }
}
