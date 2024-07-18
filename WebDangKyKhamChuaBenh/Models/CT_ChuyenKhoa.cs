using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebDangKyKhamChuaBenh.Models
{
    public class CT_ChuyenKhoa
    {
        [Key]
        [Column(Order = 0)] // Xác định MaNV là phần tử đầu tiên của khóa chính
        public int MaNV { get; set; }

        [Key]
        [Column(Order = 1)] // Xác định MaChuyenKhoa là phần tử thứ hai của khóa chính
        public int MaChuyenKhoa { get; set; }
    }
}
