using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDangKyKhamChuaBenh.Data;
using WebDangKyKhamChuaBenh.DTO;

namespace WebDangKyKhamChuaBenh.Repositories
{
    public class LichKhamBacSiRepository : ILichKhamBacSiRepository
    {
        private readonly MyAppDbContext _context;
        private readonly IMapper _mapper;

        public LichKhamBacSiRepository(MyAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<LichKhamBacSiDto>> GetAllKhamBenhAsync()
        {
            var query = from lk in _context.LichLamViec
                        join kb in _context.KhamBenh on lk.MaLichLamViec equals kb.MaLichLamViec
                        join nv in _context.NhanVien on lk.MaNV equals nv.MaNV
                        join pk in _context.PhongKham on lk.MaPhongKham equals pk.MaPhongKham
                        orderby lk.NgayBatDau, kb.GioBatDau
                        select new
                        {
                            lk,
                            kb,
                            nv,
                            pk
                        };

            var result = await query.ToListAsync();

            return _mapper.Map<List<LichKhamBacSiDto>>(result.Select(x => new LichKhamBacSiDto
            {
                NgayKham = x.lk.NgayBatDau,
                GhiChu = x.kb.GhiChu,
                MaNhanVien = x.lk.MaNV,
                GioBatDau = x.kb.GioBatDau, // Chuyển đổi từ int (giây) sang TimeSpan và định dạng thành chuỗi
                GioKetThuc = x.kb.GioKetThuc, // Chuyển đổi từ int (giây) sang TimeSpan và định dạng thành chuỗi
                MaBenhNhan = x.kb.MaBN,
                TenPhongKham = x.pk.TenPhongKham,
                Buoi = x.lk.Ca == "Ca Sáng" ? "Buổi sáng" : (x.lk.Ca == "Ca Chiều" ? "Buổi chiều" : "Không xác định")
            }).ToList());

        }

        public async Task<List<LichKhamBacSiDto>> GetKhamBenhByMaNV(int maNV)
        {
            var query = from lk in _context.LichLamViec
                        join kb in _context.KhamBenh on lk.MaLichLamViec equals kb.MaLichLamViec
                        join nv in _context.NhanVien on lk.MaNV equals nv.MaNV
                        join pk in _context.PhongKham on lk.MaPhongKham equals pk.MaPhongKham
                        where lk.MaNV == maNV
                        orderby lk.NgayBatDau, kb.GioBatDau
                        select new
                        {
                            lk,
                            kb,
                            nv,
                            pk
                        };

            var result = await query.ToListAsync();

            return _mapper.Map<List<LichKhamBacSiDto>>(result.Select(x => new LichKhamBacSiDto
            {
                NgayKham = x.lk.NgayBatDau,
                GhiChu = x.kb.GhiChu,
                MaNhanVien = x.lk.MaNV,
                GioBatDau = x.kb.GioBatDau, // Chuyển đổi từ int (giây) sang TimeSpan và định dạng thành chuỗi
                GioKetThuc = x.kb.GioKetThuc, // Chuyển đổi từ int (giây) sang TimeSpan và định dạng thành chuỗi
                MaBenhNhan = x.kb.MaBN,
                TenPhongKham = x.pk.TenPhongKham,
                Buoi = x.lk.Ca == "Ca Sáng" ? "Buổi sáng" : (x.lk.Ca == "Ca Chiều" ? "Buổi chiều" : "Không xác định")
            }).ToList());
        }
    }
}
