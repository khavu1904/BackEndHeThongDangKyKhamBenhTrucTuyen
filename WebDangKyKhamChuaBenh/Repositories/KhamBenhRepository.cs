using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebDangKyKhamChuaBenh.Data;
using WebDangKyKhamChuaBenh.Models;

namespace WebDangKyKhamChuaBenh.Repositories
{
    public class KhamBenhRepository : IKhamBenhRepository
    {
        private readonly MyAppDbContext _context;
        private readonly IMapper _mapper;

        public KhamBenhRepository(MyAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<KhamBenh> AddKhamBenhAsync(KhamBenh khamBenh)
        {
            _context.KhamBenh.Add(khamBenh);
            await _context.SaveChangesAsync();
            return khamBenh;
        }

        public Task<List<KhamBenh>> GetAllKhamBenhsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<KhamBenh>> GetAllNhomQuyensAsync()//Lấy tất cả thông tin sách
        {
            var khambenhs = await _context.KhamBenh!.ToListAsync();
            return _mapper.Map<List<KhamBenh>>(khambenhs);
        }

        public async Task<KhamBenh> GetKhamBenhAsync(int id)
        {
            var khambenh = await _context.KhamBenh!.FindAsync(id);
            return _mapper.Map<KhamBenh>(khambenh);
        }

        public async Task<KhamBenh> GetKhamBenhByIdAsync(int maKhamBenh)
        {
            return await _context.KhamBenh.FindAsync(maKhamBenh);
        }
        public async Task<IEnumerable<KhamBenh>> GetKhamBenhByTenNhanVienDuyetAsync(string tenNhanVienDuyet)
        {
            return await _context.KhamBenh
                                 .Where(kb => kb.TenNhanVienDuyet == tenNhanVienDuyet)
                                 .ToListAsync();
        }

        public async Task<List<KhamBenhDetail>> GetKhamBenhDetailsByMaBNAsync(int maBN)
        {
            var query = from kb in _context.KhamBenh
                        join nv in _context.NhanVien on kb.MaLichLamViec equals nv.MaNV
                        join llv in _context.LichLamViec on kb.MaLichLamViec equals llv.MaLichLamViec
                        where kb.MaBN == maBN
                        orderby kb.GioBatDau descending
                        select new KhamBenhDetail
                        {
                            MaKhamBenh = kb.MaKhamBenh,
                            TrieuChung = kb.TrieuChung,
                            ChuanDoan = kb.ChuanDoan,
                            GhiChu = kb.GhiChu,
                            GioBatDau = kb.GioBatDau,
                            GioKetThuc = kb.GioKetThuc,
                            NgayBatDauLichLamViec = llv.NgayBatDau,
                            TenBacSi = nv.HoTen
                        };

            return await query.ToListAsync();
        }

        public async Task UpdateKhamBenhAsync(int id, KhamBenh model)
        {

            if (id == model.MaKhamBenh)
            {
                var updateTrangThai = _mapper.Map<KhamBenh>(model);
                _context.KhamBenh!.Update(updateTrangThai);
                await _context.SaveChangesAsync();
            }
        }

        public Task UpdateKhamBenhAsync(int id, NhomQuyen model)
        {
            throw new NotImplementedException();
        }
    }
}
