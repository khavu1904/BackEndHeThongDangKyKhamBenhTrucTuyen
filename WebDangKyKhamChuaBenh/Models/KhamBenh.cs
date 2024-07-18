using System.ComponentModel.DataAnnotations;

namespace WebDangKyKhamChuaBenh.Models
{
    public class KhamBenh
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
