using Microsoft.EntityFrameworkCore;
using WebDangKyKhamChuaBenh.Models;

namespace WebDangKyKhamChuaBenh.Data
{
    public class MyAppDbContext : DbContext
    {
        public MyAppDbContext(DbContextOptions<MyAppDbContext> options) : base(options)
        {
        }
        public DbSet<NhomQuyen> NhomQuyen { get; set; }
        public DbSet<BenhNhan> BenhNhan { get; set; }
        public DbSet<ChuyenKhoa> ChuyenKhoa {  get; set; }
        public DbSet<NhanVien> NhanVien { get; set; }
        public DbSet<KhamBenh> KhamBenh { get; set; }
        public DbSet<BacSiInfo> BacSiInfo { get; set; }
        public DbSet<LichLamViec> LichLamViec {  get; set; }
        public DbSet<CT_ChuyenKhoa> CT_ChuyenKhoa { get; set; }
        public DbSet<PhongKham> PhongKham { get; set; }
        public DbSet<LichKhamBacSi> LichKhamBacSi { get; set; }
        public DbSet<KhamBenhDetail> KhamBenhDetail { get; set; }
        public DbSet<BacSiChuyenKhoa> BacSiChuyenKhoa { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder) // Xác định khóa ngoại cho đối tượng CT_Chuyên Khoa
        {
            modelBuilder.Entity<CT_ChuyenKhoa>()
                .HasKey(ctk => new { ctk.MaNV, ctk.MaChuyenKhoa });

            modelBuilder.Entity<LichKhamBacSi>().HasNoKey();
            modelBuilder.Entity<KhamBenhDetail>().HasNoKey();
            modelBuilder.Entity<BacSiChuyenKhoa>().HasNoKey();
        }
    }
}
