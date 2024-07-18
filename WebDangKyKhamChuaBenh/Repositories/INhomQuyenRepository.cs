using WebDangKyKhamChuaBenh.Data;
using WebDangKyKhamChuaBenh.Models;

namespace WebDangKyKhamChuaBenh.Repositories
{
    public interface INhomQuyenRepository
    {
        public Task<List<NhomQuyen>> GetAllNhomQuyensAsync();
        public Task<NhomQuyen> GetNhomQuyenAsync(int id);
        public Task<int> AddNhomQuyenAsync(NhomQuyen model);
        public Task UpdateNhomQuyenAsync(int id, NhomQuyen model);
        public Task DeleteNhomQuyenAsync(int id);

    }
}
