using System.ComponentModel.DataAnnotations;

namespace WebDangKyKhamChuaBenh.Models
{
    public class DangNhap
    {
        [Required]
        public string SDT { get; set; }
        [Required]
        public string MatKhau { get; set; }
    }
}
