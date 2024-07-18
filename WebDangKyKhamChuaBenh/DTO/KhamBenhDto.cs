using System.ComponentModel.DataAnnotations;
using WebDangKyKhamChuaBenh.Models;

namespace WebDangKyKhamChuaBenh.DTO
{
    public class KhamBenhDto
    {
        [Key]
        public int MaKhamBenh { get; set; }
        [Required]
        public string TrieuChung { get; set; }
        [Required]
        public string ChuanDoan { get; set; }
        [Required]
        public string GhiChu { get; set; }
        private int _gioBatDauSeconds;
        [Required]
        public TimeSpan GioBatDau
        {
            get => TimeSpan.FromSeconds(_gioBatDauSeconds);
            set => _gioBatDauSeconds = (int)value.TotalSeconds;
        }

        private int _gioKetThucSeconds;
        [Required]
        public TimeSpan GioKetThuc
        {
            get => TimeSpan.FromSeconds(_gioKetThucSeconds);
            set => _gioKetThucSeconds = (int)value.TotalSeconds;
        }
        [Required]
        public int MaBN { get; set; }
        [Required]
        public int MaLichLamViec { get; set; }
        [Required]
        public string TrangThai { get; set; }
        [Required]
        public string TenNhanVienDuyet { get; set; }
        //
        public void SetGioBatDauFromString(string gioBatDau)
        {
            GioBatDau = TimeSpan.Parse(gioBatDau);
        }

        public void SetGioKetThucFromString(string gioKetThuc)
        {
            GioKetThuc = TimeSpan.Parse(gioKetThuc);
        }
        //
    }
}
