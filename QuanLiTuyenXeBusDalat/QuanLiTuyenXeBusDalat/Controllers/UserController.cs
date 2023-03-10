using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using QuanLiTuyenXeBusDalat.Data;
using QuanLiTuyenXeBusDalat.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace QuanLiTuyenXeBusDalat.Controllers
{
    // Đã xong
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly MyDBContext _context;
        private readonly AppSettings _appSettings;

        public UserController(MyDBContext myDBContext, IOptionsMonitor<AppSettings> optionsMonitor)
        {
            _context = myDBContext;
            _appSettings = optionsMonitor.CurrentValue;
        }
        [HttpPost("Login")]
        public IActionResult validate(LoginModel loginModel)
        {
            var user = _context.taiKhoans.SingleOrDefault(p => p.UserName==loginModel.UserName && loginModel.Password==p.Password);

            if (user == null)
            {
                return Ok(new ApiResponse
                {
                    Success = false,
                    Message = "Invalid Username/password"
                });
            }
            // Cấp token
            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Authenticate success",
                Data = user
            });
        }

        // Sinh mã token
        private string GenerateToken(TaiKhoan taiKhoan)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var setcretKeyByte = Encoding.UTF8.GetBytes(_appSettings.SecretKey);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name,taiKhoan.HoTen),
                    new Claim(ClaimTypes.Email, taiKhoan.Email),
                    new Claim("UserName", taiKhoan.UserName),
                    new Claim("Id", taiKhoan.MaTaiKhoan.ToString()),

                    //roles
                    new Claim("TokenId", Guid.NewGuid().ToString())
                }),

                //Thực hiện việc refresh token sau 1 phút
                Expires = DateTime.UtcNow.AddMinutes(1),//TIme out
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(setcretKeyByte), SecurityAlgorithms.HmacSha512Signature)
            };
            var token = jwtTokenHandler.CreateToken(tokenDescription);
            return jwtTokenHandler.WriteToken(token);
        }

    }
}
