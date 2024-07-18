using WebDangKyKhamChuaBenh.DTO;
using WebDangKyKhamChuaBenh.Models;

namespace WebDangKyKhamChuaBenh.Repositories
{
    public interface ILichKhamBacSiRepository
    {
        Task<List<LichKhamBacSiDto>> GetKhamBenhByMaNV(int maNV);
        Task<List<LichKhamBacSiDto>> GetAllKhamBenhAsync();
    }
}
