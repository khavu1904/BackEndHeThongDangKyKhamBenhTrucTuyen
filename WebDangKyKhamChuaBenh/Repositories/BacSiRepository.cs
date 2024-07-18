using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebDangKyKhamChuaBenh.Data;
using WebDangKyKhamChuaBenh.DTO;
using WebDangKyKhamChuaBenh.Models;

namespace WebDangKyKhamChuaBenh.Repositories
{
    public class BacSiRepository : IBacSiRepository
    {
        private readonly MyAppDbContext _context;
        private readonly IMapper _mapper;

        public BacSiRepository(MyAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActionResult<BacSiChuyenKhoa>> GetBacSiChuyenKhoa(int maNV)
        {
            var bacSiChuyenKhoa = await _context.NhanVien
             .Where(bs => bs.MaNV == maNV)
             .Join(_context.CT_ChuyenKhoa, bs => bs.MaNV, ctk => ctk.MaNV, (bs, ctk) => new { BacSi = bs, CTChuyenKhoa = ctk })
             .Join(_context.ChuyenKhoa, combined => combined.CTChuyenKhoa.MaChuyenKhoa, ck => ck.MaChuyenKhoa, (combined, ck) => new BacSiChuyenKhoa
             {
                 TenBacSi = combined.BacSi.HoTen,
                 AnhDaiDien = combined.BacSi.AnhDaiDien,
                 Tuoi = combined.BacSi.Tuoi,
                 HocVan = combined.BacSi.HocVan,
                 Email = combined.BacSi.Email,
                 TenChuyenKhoa = ck.TenChuyenKhoa
             })
             .FirstOrDefaultAsync();

                return bacSiChuyenKhoa;
            }

        public async Task<IEnumerable<BacSiInfo>> GetBacSiWithChuyenKhoa()
        {
            var bacSiInfoDtos = await _context.NhanVien
                .Join(_context.CT_ChuyenKhoa, nv => nv.MaNV, ctk => ctk.MaNV, (nv, ctk) => new { NhanVien = nv, CT_ChuyenKhoa = ctk })
                .Join(_context.ChuyenKhoa, temp => temp.CT_ChuyenKhoa.MaChuyenKhoa, ck => ck.MaChuyenKhoa, (temp, ck) => new { temp.NhanVien, ChuyenKhoa = ck })
                .Join(_context.LichLamViec, temp => temp.NhanVien.MaNV, llv => llv.MaNV, (temp, llv) => new BacSiInfoDto
                {
                    MaNV = temp.NhanVien.MaNV,
                    HocVan = temp.NhanVien.HocVan,
                    TenBacSi = temp.NhanVien.HoTen,
                    TenChuyenKhoa = temp.ChuyenKhoa.TenChuyenKhoa,
                    MaChuyenKhoa = temp.ChuyenKhoa.MaChuyenKhoa,
                    MaLichLamViec = llv.MaLichLamViec,
                    Ca = llv.Ca,
                    NgayBatDau = llv.NgayBatDau,
                    AnhDaiDien = temp.NhanVien.AnhDaiDien
                })
                .ToListAsync();

            var bacSiInfos = _mapper.Map<IEnumerable<BacSiInfo>>(bacSiInfoDtos);

            return bacSiInfos;
        }

        public async Task<IEnumerable<BacSiInfo>> GetBacSiWithChuyenKhoaId(int maChuyenKhoa)
        {
            var result = await _context.NhanVien
                .Join(_context.CT_ChuyenKhoa, nv => nv.MaNV, ctk => ctk.MaNV, (nv, ctk) => new { nv, ctk })
                .Join(_context.ChuyenKhoa, temp => temp.ctk.MaChuyenKhoa, ck => ck.MaChuyenKhoa, (temp, ck) => new { temp, ck })
                .Join(_context.LichLamViec, temp2 => temp2.temp.nv.MaNV, llv => llv.MaNV, (temp2, llv) => new BacSiInfo
                {
                    MaNV = temp2.temp.nv.MaNV,
                    HocVan = temp2.temp.nv.HocVan,
                    TenBacSi = temp2.temp.nv.HoTen,
                    TenChuyenKhoa = temp2.ck.TenChuyenKhoa,
                    MaChuyenKhoa = temp2.ck.MaChuyenKhoa,
                    MaLichLamViec = llv.MaLichLamViec,
                    Ca = llv.Ca,
                    NgayBatDau = llv.NgayBatDau,
                    AnhDaiDien = temp2.temp.nv.AnhDaiDien
                })
                .Where(info => info.MaChuyenKhoa == maChuyenKhoa) // Lọc theo mã chuyên khoa
                .ToListAsync();

            return result;
        }
    }
}
