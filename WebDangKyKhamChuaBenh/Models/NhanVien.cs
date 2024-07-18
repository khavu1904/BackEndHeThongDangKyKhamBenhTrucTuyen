using System.ComponentModel.DataAnnotations;

namespace WebDangKyKhamChuaBenh.Models
{
    public class NhanVien
    {
        [Key]
        public int MaNV { get; set; }

        [Required]
        public string HoTen { get; set; }

        [Required]
        public string SDT { get; set; }

        [Required]
        public string DiaChi { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string MatKhau { get; set; }

        [Required]
        public string HocVan { get; set; }
        [Required]
        public int Tuoi { get; set; }
        [Required]
        public DateTime NgaySinh { get; set; }

        [Required]
        public string AnhDaiDien { get; set; }

        [Required]
        public string CCCD { get; set; }

        [Required]
        public string ChucDanh { get; set; }
        public bool TrangThai { get; set; }

        public int? MaQuyen { get; set; }
        public int? MaPhongBan { get; set; }
    }
}
