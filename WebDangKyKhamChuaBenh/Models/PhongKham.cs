using System.ComponentModel.DataAnnotations;

namespace WebDangKyKhamChuaBenh.Models
{
    public class PhongKham
    {
        [Key]
        public int MaPhongKham { get; set; }

        [Required]
        public string TenPhongKham { get; set; }

        [Required]
        public string TinhTrang { get; set; }
    }
}
