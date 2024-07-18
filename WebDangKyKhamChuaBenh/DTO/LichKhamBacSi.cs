using System.ComponentModel.DataAnnotations;

namespace WebDangKyKhamChuaBenh.DTO
{
    public class LichKhamBacSiDto
    {
        [Key]
        public int Id { get; set; }
        public DateTime NgayKham { get; set; }
        public string GhiChu { get; set; }
        public int MaNhanVien { get; set; }
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
        public int MaBenhNhan { get; set; }
        public string Buoi { get; set; }

        public void SetGioBatDauFromString(string gioBatDau)
        {
            GioBatDau = TimeSpan.Parse(gioBatDau);
        }

        public void SetGioKetThucFromString(string gioKetThuc)
        {
            GioKetThuc = TimeSpan.Parse(gioKetThuc);
        }
        public string TenPhongKham { get; set; }
    }
}
