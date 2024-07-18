using Microsoft.AspNetCore.Mvc;
using WebDangKyKhamChuaBenh.Models;

namespace WebDangKyKhamChuaBenh.Repositories
{
    public interface IBacSiRepository
    {
        Task<IEnumerable<BacSiInfo>> GetBacSiWithChuyenKhoa();
        Task<IEnumerable<BacSiInfo>> GetBacSiWithChuyenKhoaId(int maChuyenKhoa);
        Task<ActionResult<BacSiChuyenKhoa>> GetBacSiChuyenKhoa(int maNV);
    }
}
