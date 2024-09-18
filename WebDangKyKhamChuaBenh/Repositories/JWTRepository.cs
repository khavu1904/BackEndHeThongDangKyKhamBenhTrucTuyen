using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebDangKyKhamChuaBenh.Data;
using WebDangKyKhamChuaBenh.Models;
using System.Threading.Tasks;

namespace WebDangKyKhamChuaBenh.Repositories
{
    public class JWTRepository : IJWTRepository
    {
        private readonly MyAppDbContext _context;
        private readonly IMapper _mapper;

        public JWTRepository(MyAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddBenhNhanAsync(BenhNhan model)
        {
            _context.BenhNhan.Add(model);
            await _context.SaveChangesAsync();
        }

        public async Task<BenhNhan> AuthenticateAsync(string sdt, string MatKhau)
        {
            var benhnhan = await _context.BenhNhan
                .Where(x => x.SDT == sdt && x.MatKhau == MatKhau)
                .Select(x => new BenhNhan
                {
                    MaBN = x.MaBN,
                    HoTenBN = x.HoTenBN,
                    DiaChi = x.DiaChi,
                    CMND_CCCD = x.CMND_CCCD,
                    SDT = x.SDT,
                    NgaySinh = x.NgaySinh,
                    GioiTinh = x.GioiTinh,
                    MaBHYT = x.MaBHYT,
                    DanToc = x.DanToc,
                    NgheNghiep = x.NgheNghiep,
                    Email = x.Email,
                    AnhDaiDien = x.AnhDaiDien,
                    MatKhau = x.MatKhau,
                    MaQuyen = x.MaQuyen
                })
                .SingleOrDefaultAsync();

            return benhnhan;
        }

        public async Task<bool> BNExistsAsync(string sdt)
        {
            return await _context.BenhNhan.AnyAsync(x => x.SDT == sdt);
        }
        public async Task<int> AddBenhNhansAsync(BenhNhan model)
        {
            _context.BenhNhan.Add(model);
            await _context.SaveChangesAsync();
            return model.MaBN; // Trả về MaBN sau khi lưu vào cơ sở dữ liệu
        }
        public async Task UpdateBenhNhanAsync(int maBN, BenhNhan model) /*Cập nhật lại thông tin trong jwt của hệ thống*/
        {
            var existingBenhNhan = await _context.BenhNhan.FindAsync(maBN);
            if (existingBenhNhan == null)
            {
                // Trả về một thông báo lỗi nếu không tìm thấy bệnh nhân
                throw new Exception("Bệnh nhân không tồn tại!");
            }

            // Cập nhật thông tin của bệnh nhân
            existingBenhNhan.HoTenBN = model.HoTenBN;
            existingBenhNhan.DiaChi = model.DiaChi;
            existingBenhNhan.CMND_CCCD = model.CMND_CCCD;
            existingBenhNhan.NgaySinh = model.NgaySinh;
            existingBenhNhan.GioiTinh = model.GioiTinh;
            existingBenhNhan.MaBHYT = model.MaBHYT;
            existingBenhNhan.DanToc = model.DanToc;
            existingBenhNhan.NgheNghiep = model.NgheNghiep;
            existingBenhNhan.Email = model.Email;
            existingBenhNhan.AnhDaiDien = model.AnhDaiDien;
            existingBenhNhan.MatKhau = model.MatKhau;

            // Lưu thay đổi vào cơ sở dữ liệu
            await _context.SaveChangesAsync();
        }
        public async Task<NhanVien> AuthenticateAsyncNV(string sdt, string MatKhau)
        {
            var nhanvien = await _context.NhanVien
                .Where(x => x.SDT == sdt && x.MatKhau == MatKhau)
                .Select(x => new NhanVien
                {
                    MaNV = x.MaNV,
                    HoTen = x.HoTen,
                    DiaChi = x.DiaChi,
                    SDT = x.SDT,
                    Email = x.Email,
                    MatKhau = x.MatKhau,
                    HocVan = x.HocVan,
                    NgaySinh = x.NgaySinh,
                    AnhDaiDien = x.AnhDaiDien,
                    ChucDanh = x.ChucDanh,
                    MaQuyen = x.MaQuyen,
                })
                .SingleOrDefaultAsync();

            return nhanvien;
        }

    }

}
