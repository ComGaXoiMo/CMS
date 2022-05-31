using CMS_1.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CMS_1.System
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;

        public UserService(IConfiguration configuration, AppDbContext appDbContext)
        {
            _configuration = configuration;
            _context = appDbContext;
        }
        public LoginResponse Authenticate(LoginRequest model)
        {
            var user = _context.Users;
                //.SingleOrDefault(u=> u.Email== model.UserName && u.Password==model.Password);
            if(!user.Any(u => u.Email == model.UserName && u.Password == model.Password))
            {
                return null;
            }
            var claim = new Claim[]
                {
                    new Claim(ClaimTypes.Name, model.UserName)
                };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSecurityKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.UtcNow.AddHours(Convert.ToInt32(_configuration["JwtExpiryInDays"]));

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtAudience"],
                claim,
                expires: expiry,
                signingCredentials: creds
                );

            return new LoginResponse {Success = true, Token= new JwtSecurityTokenHandler().WriteToken(token) }; 

        }
    }
}
