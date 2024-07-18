using WebDangKyKhamChuaBenh.Models;
using System.Threading.Tasks;

namespace WebDangKyKhamChuaBenh.Repositories
{
    public interface IJWTRepository
    {
        Task<BenhNhan> AuthenticateAsync(string sdt, string password);
        Task<bool> BNExistsAsync(string sdt);
        Task AddBenhNhanAsync(BenhNhan model);
        Task<int> AddBenhNhansAsync(BenhNhan model);
        Task UpdateBenhNhanAsync(int maBN, BenhNhan model);
        Task<NhanVien> AuthenticateAsyncNV(string sdt, string password);
    }
}
