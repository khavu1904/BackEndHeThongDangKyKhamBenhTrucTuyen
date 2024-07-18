using System.ComponentModel.DataAnnotations;

namespace WebDangKyKhamChuaBenh.DTO
{
    public class DangNhapDto
    {
        [Required]
        public string SDT { get; set; }
        [Required]
        public string MatKhau { get; set; }
    }
}
