using System.ComponentModel.DataAnnotations;

namespace WebDangKyKhamChuaBenh.Models
{
    public class BacSiChuyenKhoa
    {
        [Key]
        public int Id { get; set; }
        public string TenBacSi { get; set; }
        public int Tuoi { get; set; }
        public string HocVan { get; set; }
        public string Email { get; set; }
        public string TenChuyenKhoa { get; set; }
        public string AnhDaiDien { get; set; }
    }
}
