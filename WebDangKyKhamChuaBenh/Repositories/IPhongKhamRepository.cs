using WebDangKyKhamChuaBenh.Models;

namespace WebDangKyKhamChuaBenh.Repositories
{
    public interface IPhongKhamRepository
    {
        Task<List<PhongKham>> GetAllPhongKhamAsync();
    }
}
