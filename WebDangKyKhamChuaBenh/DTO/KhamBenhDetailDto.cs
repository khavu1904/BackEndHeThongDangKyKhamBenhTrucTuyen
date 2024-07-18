using System.ComponentModel.DataAnnotations;

namespace WebDangKyKhamChuaBenh.DTO
{
    public class KhamBenhDetailDto
    {
        [Key]
        public int Id { get; set; }
        public int MaKhamBenh { get; set; }

        [Required(ErrorMessage = "TrieuChung is required")]
        public string TrieuChung { get; set; }

        [Required(ErrorMessage = "ChuanDoan is required")]
        public string ChuanDoan { get; set; }

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
        public DateTime NgayBatDauLichLamViec { get; set; }

        public string TenBacSi { get; set; }
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
