using System.ComponentModel.DataAnnotations;

namespace WebDangKyKhamChuaBenh.Models
{
    public class ChuyenKhoa
    {
        [Key]
        public int MaChuyenKhoa { get; set; }
        [Required]
        public string TenChuyenKhoa { get; set; }
        [Required]
        public string VietTat {  get; set; }
    }
}
