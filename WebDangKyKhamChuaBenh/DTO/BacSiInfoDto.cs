using System.ComponentModel.DataAnnotations;

namespace WebDangKyKhamChuaBenh.DTO
{
    public class BacSiInfoDto
    {
        public int Id { get; set; }
        public int MaNV { get; set; }
        public string TenBacSi { get; set; }
        public string TenChuyenKhoa { get; set; }
        public string HocVan { get; set; }
        public int MaLichLamViec { get; set; }
        public int MaChuyenKhoa { get; set; }
        public string AnhDaiDien { get; set; }
        public string Ca { get; set; }
        [DataType(DataType.Date)]
        public DateTime NgayBatDau { get; set; }
    }
}
