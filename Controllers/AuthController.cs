using EventProjectApi.DTOs.AdminDtos;
using EventProjectApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EventProjectApi.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IConfiguration configuration;
        public AuthController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public static Admin admin = new();

        /// <summary>
        /// API to register new admin ( User )
        /// </summary>
        /// <param name="adminDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("register")]
        public ActionResult<Admin> Register(AdminDto adminDto)
        {
            var hashedPassword = new PasswordHasher<Admin>().HashPassword(admin, adminDto.Password);
            admin.UserName = adminDto.UserName;
            admin.Password = hashedPassword;

            return Ok(admin);
        }

        /// <summary>
        /// API to login admin ( User )
        /// </summary>
        /// <param name="adminDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("login")]
        public ActionResult<string> login(AdminDto adminDto)
        {
            string username = adminDto.UserName;
            string password = adminDto.Password;

            if ( admin.UserName != username )
            {
                return BadRequest("User not found !");
            }

            var passwordHasher = new PasswordHasher<Admin>();
            if ( passwordHasher.VerifyHashedPassword(admin, admin.Password, password) == PasswordVerificationResult.Failed )
            {
                return BadRequest("Password not correct !");
            }

            return Ok(CreateToken(admin));
        }

        /// <summary>
        /// Function to create JWT Token
        /// </summary>
        /// <param name="admin"></param>
        /// <returns></returns>
        private string CreateToken(Admin admin)
        {
            // Create Your Claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, admin.UserName),
            };

            // Create Key
            var key = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(configuration.GetValue<string>("JwtSettings:key")!
                )
            );

            // Create Credentials
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            // Generate Token Descriptor
            var tokenDescriptor = new JwtSecurityToken(
                issuer: configuration.GetValue<string>("JwtSettings:issuer"),    
                audience: configuration.GetValue<string>("JwtSettings:audience"),
                claims: claims,
                signingCredentials: credentials,
                expires: DateTime.UtcNow.AddDays(1)
            );

            var tokem = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

            return tokem;
        }
    }
}
