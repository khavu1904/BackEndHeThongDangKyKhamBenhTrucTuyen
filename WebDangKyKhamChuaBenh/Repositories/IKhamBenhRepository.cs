using WebDangKyKhamChuaBenh.Models;

namespace WebDangKyKhamChuaBenh.Repositories
{
    public interface IKhamBenhRepository
    {
        Task<KhamBenh> AddKhamBenhAsync(KhamBenh khamBenh);
        Task<KhamBenh> GetKhamBenhByIdAsync(int maKhamBenh);
        Task<List<KhamBenh>> GetAllKhamBenhsAsync();
        Task<KhamBenh> GetKhamBenhAsync(int id);
        Task<List<KhamBenhDetail>> GetKhamBenhDetailsByMaBNAsync(int maBN);
        Task UpdateKhamBenhAsync(int id, NhomQuyen model);
        Task<IEnumerable<KhamBenh>> GetKhamBenhByTenNhanVienDuyetAsync(string tenNhanVienDuyet);

    }
}
