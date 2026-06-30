using BCrypt.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ExpenseTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;
        public AuthController(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        [HttpPost("register")]
        public IActionResult Register(User user)
        {
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
            _context.Users.Add(user);
            _context.SaveChanges();
            return Ok("User Registered SuccessFully");
        }

        [HttpPost("login")]
        public IActionResult Login(User loginuser)
        {
            var user = _context.Users.FirstOrDefault(u=>u.UserName==loginuser.UserName);
            if (user == null)
            {
                return Unauthorized("Invalid Username Or Password");
            }
            bool ispassword = BCrypt.Net.BCrypt.Verify(loginuser.PasswordHash, user.PasswordHash);
            if (!ispassword)
                return Unauthorized("Invalid Username Or Passoword");
            //Generating JWT Token.
            var claims = new[]
            {
                new Claim(ClaimTypes.Name,user.UserName!),
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString())
            };

            var jwtKey = _config["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key is missing in configuration");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
        }
    }
}