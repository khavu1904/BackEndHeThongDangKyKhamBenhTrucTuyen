using System.ComponentModel.DataAnnotations;
using WebDangKyKhamChuaBenh.Models;

namespace WebDangKyKhamChuaBenh.DTO
{
    public class LichLamViecDto
    {
        [Key]
        public int MaLichLamViec { get; set; }

        public string Ca { get; set; }
        [DataType(DataType.Date)]
        public DateTime NgayTao { get; set; }
        [DataType(DataType.Date)]
        public DateTime NgayBatDau { get; set; }

        public int MaNV { get; set; }

        public int MaPhongKham { get; set; }
        public string TenNhanVienTao { get; set; }
    }
}
