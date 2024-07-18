using AutoMapper;
using WebDangKyKhamChuaBenh.DTO;
using WebDangKyKhamChuaBenh.Models;

namespace WebDangKyKhamChuaBenh.Helpers
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<NhomQuyen, NhomQuyenDto>().ReverseMap();
            CreateMap<BenhNhan, BenhNhanDto>().ReverseMap();
            CreateMap<ChuyenKhoa, ChuyenKhoaDto>().ReverseMap();
            CreateMap<NhanVien, NhanVienDto>().ReverseMap();
            CreateMap<DangKy, DangKyDto>().ReverseMap();
            CreateMap<DangNhap, DangNhapDto>().ReverseMap();
            CreateMap<KhamBenh, KhamBenhDto>().ReverseMap();
            CreateMap<BacSiInfo, BacSiInfoDto>().ReverseMap();
            CreateMap<LichLamViec, LichLamViec>().ReverseMap();
            CreateMap<NhanVien, NhanVienDto>().ReverseMap();
            CreateMap<DangNhapNV, DangNhapNVDto>().ReverseMap();
            CreateMap<Email, EmailDto>().ReverseMap();
            CreateMap<PhongKham, PhongKhamDto>().ReverseMap();
            CreateMap<KhamBenhDetail, KhamBenhDetailDto>().ReverseMap();
            CreateMap<BacSiChuyenKhoa, BacSiChuyenKhoaDto>().ReverseMap();
            //
            CreateMap<LichLamViec, LichKhamBacSiDto>()
              .ForMember(dest => dest.NgayKham, opt => opt.MapFrom(src => src.NgayBatDau))
              .ForMember(dest => dest.MaNhanVien, opt => opt.MapFrom(src => src.MaNV))
              .ForMember(dest => dest.GioBatDau, opt => opt.Ignore()) // Bỏ qua hoặc ánh xạ nếu cần
              .ForMember(dest => dest.GioKetThuc, opt => opt.Ignore())
              .ForMember(dest => dest.MaBenhNhan, opt => opt.Ignore())
              .ForMember(dest => dest.GhiChu, opt => opt.Ignore())
              .ForMember(dest => dest.TenPhongKham, opt => opt.Ignore())
              .ForMember(dest => dest.Buoi, opt => opt.MapFrom(src =>
                  src.Ca == "Sáng" ? "Buổi sáng" : (src.Ca == "Chiều" ? "Buổi chiều" : "Không xác định")));
        }
    }
}
