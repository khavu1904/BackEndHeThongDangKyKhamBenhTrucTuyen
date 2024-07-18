using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebDangKyKhamChuaBenh.Data;
using WebDangKyKhamChuaBenh.Models;
using WebDangKyKhamChuaBenh.Repositories;
using System.Security.Cryptography;

namespace WebDangKyKhamChuaBenh.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IJWTRepository _jwtRepository;
        private readonly IConfiguration _configuration;

        public AuthenticateController(IJWTRepository jwtRepository, IConfiguration configuration)
        {
            _jwtRepository = jwtRepository;
            _configuration = configuration;
        }
        public class PasswordHelper
        {
            public static string HashPassword(string MatKhau)
            {
                using (var sha256 = SHA256.Create())
                {
                    var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(MatKhau));
                    return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                }
            }

            public static bool VerifyPassword(string MatKhau, string hashedPassword)
            {
                return HashPassword(MatKhau) == hashedPassword;
            }
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] DangNhap model)
        {
            // Mã hóa mật khẩu người dùng nhập
            string hashedPassword = PasswordHelper.HashPassword(model.MatKhau);

            // Thực hiện xác thực với mật khẩu đã mã hóa
            var benhnhan = await _jwtRepository.AuthenticateAsync(model.SDT, hashedPassword);
            if (benhnhan == null)
            {
                // Nếu không có bệnh nhân hoặc mật khẩu không khớp, trả về Unauthorized
                return Unauthorized(new Response { Status = "Error", Message = "Invalid credentials" });
            }

            // Xác thực thành công, tạo mã thông báo JWT
            var token = GenerateJwtToken(benhnhan, benhnhan.MaBN);

            // Trả về mã thông báo JWT và hạn chế thời gian
            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] DangKy model)
        {
            if (await _jwtRepository.BNExistsAsync(model.SDT))
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Patient already exists!" });

            var benhnhan = new BenhNhan
            {
                HoTenBN = model.HoTenBN,
                DiaChi = model.DiaChi,
                CMND_CCCD = model.CMND_CCCD,
                SDT = model.SDT,
                NgaySinh = model.NgaySinh,
                GioiTinh = model.GioiTinh,
                MaBHYT = model.MaBHYT,
                DanToc = model.DanToc,
                NgheNghiep = model.NgheNghiep,
                Email = model.Email,
                AnhDaiDien = model.AnhDaiDien,
                MatKhau = model.MatKhau,
                MaQuyen = 1 // Gán giá trị mặc định là 1
            };
            int maBN = await _jwtRepository.AddBenhNhansAsync(benhnhan);

            var token = GenerateJwtToken(benhnhan, maBN);

            return Ok(new
            {
                Status = "Success",
                Message = "Đăng ký thành công!",
            });
        }

        private JwtSecurityToken GenerateJwtToken(BenhNhan benhnhan, int maBN)
        {
            var authClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, benhnhan.SDT.ToString()),
                new Claim("HoTenBN", benhnhan.HoTenBN ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Email, benhnhan.Email ?? string.Empty),
                new Claim("CMND_CCCD", benhnhan.CMND_CCCD ?? string.Empty),
                new Claim("MaBHYT", benhnhan.MaBHYT ?? string.Empty),
                new Claim("MaQuyen", benhnhan.MaQuyen?.ToString() ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("MaBN", maBN.ToString()) // Thêm MaBN vào claims
            };

            if (benhnhan.NgaySinh.HasValue)
            {
                authClaims.Add(new Claim("NgaySinh", benhnhan.NgaySinh.Value.ToString("yyyy-MM-dd")));
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));

            var token = new JwtSecurityToken(
                  issuer: _configuration["Jwt:ValidIssuer"],
                  audience: "BenhNhan", // Đặt audience là "NhanVien"
                  expires: DateTime.Now.AddHours(3),
                  claims: authClaims,
                  signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
              );
            return token;
        }
        [HttpPut]
        [Route("update/{maBN}")]
        public async Task<IActionResult> UpdateByMaBN(int maBN, [FromBody] BenhNhan model)
        {
            try
            {
                await _jwtRepository.UpdateBenhNhanAsync(maBN, model);
                return Ok(new Response { Status = "Success", Message = "Thông tin bệnh nhân đã được cập nhật!" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = ex.Message });
            }
        }
        /**/
        private JwtSecurityToken GenerateJwtTokenNhanVien(NhanVien nhanvien, int maNV)
        {
            var authClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, nhanvien.SDT.ToString()),
                new Claim("HoTen", nhanvien.HoTen ?? string.Empty),
                new Claim("ChucDanh", nhanvien.ChucDanh ?? string.Empty),
                new Claim("MaQuyen", nhanvien.MaQuyen?.ToString() ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("MaNV", maNV.ToString()) // Thêm MaBN vào claims
            };

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));

            var token = new JwtSecurityToken(
                 issuer: _configuration["Jwt:ValidIssuer"],
                 audience: "NhanVien", // Đặt audience là "NhanVien"
                 expires: DateTime.Now.AddHours(3),
                 claims: authClaims,
                 signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
             );


            return token;
        }
        [HttpPost]
        [Route("login/nhanvien")]
        public async Task<IActionResult> LoginNV([FromBody] DangNhap model)
        {
            // Mã hóa mật khẩu người dùng nhập
            string hashedMatKhau = PasswordHelper.HashPassword(model.MatKhau);

            // Thực hiện xác thực với mật khẩu đã mã hóa
            var nhanvien = await _jwtRepository.AuthenticateAsyncNV(model.SDT, hashedMatKhau);
            if (nhanvien == null)
            {
                // Nếu không có bệnh nhân hoặc mật khẩu không khớp, trả về Unauthorized
                return Unauthorized(new Response { Status = "Error", Message = "Invalid credentials" });
            }

            // Xác thực thành công, tạo mã thông báo JWT
            var token = GenerateJwtTokenNhanVien(nhanvien, nhanvien.MaNV);

            // Trả về mã thông báo JWT và hạn chế thời gian
            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });
            //var nhanvien = await _jwtRepository.AuthenticateAsyncNV(model.SDT, model.Password);
            //if (nhanvien == null)
            //    return Unauthorized(new Response { Status = "Error", Message = "Invalid credentials" });

            //var token = GenerateJwtTokenNhanVien(nhanvien, nhanvien.MaNV);

            //return Ok(new
            //{
            //    token = new JwtSecurityTokenHandler().WriteToken(token),
            //    expiration = token.ValidTo
            //});
        }
        /**/
    }
}
