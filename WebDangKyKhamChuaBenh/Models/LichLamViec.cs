using System.ComponentModel.DataAnnotations;

namespace WebDangKyKhamChuaBenh.Models
{
    public class LichLamViec
    {
        [Key]
        public int MaLichLamViec { get; set; }

        public string Ca {  get; set; }
        [DataType(DataType.Date)]
        public DateTime NgayTao { get; set; }
        [DataType(DataType.Date)]
        public DateTime NgayBatDau { get; set; }

        public int MaNV { get; set; }

        public int MaPhongKham {  get; set; }
        public string TenNhanVienTao { get; set; }
    }
}
