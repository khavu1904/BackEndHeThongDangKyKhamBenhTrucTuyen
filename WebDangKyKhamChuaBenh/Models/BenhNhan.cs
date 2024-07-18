using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebDangKyKhamChuaBenh.Models
{
    public class BenhNhan
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
        public int Tuoi { get; set; }

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

        public int? MaQuyen { get; set; }

    }
}
