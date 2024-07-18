using System.ComponentModel.DataAnnotations;
using WebDangKyKhamChuaBenh.Models;

namespace WebDangKyKhamChuaBenh.DTO
{
    public class PhongKhamDto
    {
        [Key]
        public int MaPhongKham { get; set; }

        [Required]
        public string TenPhongKham { get; set; }

        [Required]
        public string TinhTrang { get; set; }
    }
}
