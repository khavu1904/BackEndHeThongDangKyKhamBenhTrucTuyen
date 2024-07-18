using System.ComponentModel.DataAnnotations;

namespace WebDangKyKhamChuaBenh.DTO
{
    public class ChuyenKhoaDto
    {
        [Key]
        public int MaChuyenKhoa { get; set; }
        [Required]
        public string TenChuyenKhoa { get; set; }
        [Required]
        public string VietTat {  get; set; }
    }
}
