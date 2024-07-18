using System.ComponentModel.DataAnnotations;

namespace WebDangKyKhamChuaBenh.Models
{
    public class NhomQuyen
    {
        [Key]
        public int MaQuyen { get; set; }
        public string TenQuyen { get; set; }
    }
}
