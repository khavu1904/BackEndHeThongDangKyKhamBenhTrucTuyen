using System.ComponentModel.DataAnnotations;

namespace WebDangKyKhamChuaBenh.DTO
{
    public class DangNhapNVDto
    {
        [Required]
        public string SDT { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
