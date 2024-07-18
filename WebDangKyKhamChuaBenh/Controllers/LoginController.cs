using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using WebDangKyKhamChuaBenh.Models;

namespace WebDangKyKhamChuaBenh.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        public LoginController(IConfiguration configuration)
        {
            _config = configuration;
        }
        private BenhNhan AuthenticateBenhNhan(BenhNhan benhnhan)
        {
            BenhNhan _benhnhan = null;
            if (benhnhan.SDT == "0378673951" && benhnhan.MatKhau=="123")
            {
                benhnhan = new BenhNhan { HoTenBN = "Kha" };
            }
            return _benhnhan;
        }
        private string GenerateToken(BenhNhan benhnhan)
        {
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt: Audience"], null,
                expires: DateTime.Now.AddMinutes(1),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(BenhNhan benhnhan)
        {
            IActionResult response = Unauthorized();
            var benhnhan_ = AuthenticateBenhNhan(benhnhan);
            if(benhnhan_ !=null)
            {
                var token = GenerateToken(benhnhan_);
                response = Ok(new { token = token });
            }
            return response;
        }
    }
}
