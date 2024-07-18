using System.ComponentModel.DataAnnotations;

namespace WebDangKyKhamChuaBenh.DTO
{
    public class DangKyDto
    {
        [Key]
        public int MaBN { get; set; }
        [Required]
        public string HoTenBN { get; set; }
        [Required]
        public string DiaChi { get; set; }
        [Required]
        public string CMND_CCCD { get; set; }
        [Required]
        public string SDT { get; set; }
        [Required]
        public DateTime? NgaySinh { get; set; }
        [Required]
        public string GioiTinh { get; set; }
        [Required]
        public string MaBHYT { get; set; }
        [Required]
        public string DanToc { get; set; }
        [Required]
        public string NgheNghiep { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string AnhDaiDien { get; set; }
        [Required]
        public string MatKhau { get; set; }
        [Required]
        public int MaQuyen { get; set; }
    }
}
