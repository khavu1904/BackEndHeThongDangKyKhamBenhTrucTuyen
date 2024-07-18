using System.ComponentModel.DataAnnotations;

namespace WebDangKyKhamChuaBenh.Models
{
    public class DangNhapNV
    {
        [Required]
        public string SDT { get; set; }
        [Required]
        public string MatKhau { get; set; }
    }
}
