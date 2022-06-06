using CMS_1.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace CMS_1.System
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _dbcontext;

        public UserService(IConfiguration configuration, AppDbContext appDbContext)
        {
            _configuration = configuration;
            _dbcontext = appDbContext;
        }
        public async Task<LoginResponse>  Authenticate(LoginRequest model)
        {
            var user =  _dbcontext.Users;
                
            if(!user.Any(u => u.Email == model.UserName && u.Password == model.Password))
            {
                return new LoginResponse { Error = "Please check your email and password then try again", Success = false };
            }
            var claim = new Claim[]
                {
                    new Claim(ClaimTypes.Name, model.UserName)
                };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSecurityKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.UtcNow.AddDays(Convert.ToInt32(_configuration["JwtExpiryInDays"]));

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtAudience"],
                claim,
                expires: expiry,
                signingCredentials: creds
                );

            return new LoginResponse {Success = true, Token= new JwtSecurityTokenHandler().WriteToken(token) }; 

        }
       
        public async Task<ForgotPasswordResponse>  ForgotPassword(FogotPasswordRequest model)
        {
            var user = _dbcontext.Users;

            if (!user.Any(u => u.Email == model.Email))
            {
                return new ForgotPasswordResponse { Success = false, Message = "Please enter a valid email and try again" };
            }
            var claim = new Claim[]
                {
                    new Claim(ClaimTypes.Name, model.Email)
                };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSecurityKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.UtcNow.AddMinutes(Convert.ToInt32(_configuration["JwtExpiryInMinutes"]));

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtAudience"],
                claim,
                expires: expiry,
                signingCredentials: creds
                );

            return new ForgotPasswordResponse { Success = true
                , Message = "We've sent an email to (entered email address) Click the link in the email to reset your password."
                , Token = new JwtSecurityTokenHandler().WriteToken(token) };

        }

        public async Task<RecoverPasswordResponse>  RecoverPassword(RecoverPasswordRequest model)
        {
            Regex rx = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(.{6,})$");
            if (!rx.IsMatch(model.Password))
            {
                return new RecoverPasswordResponse { Success = false, Message = "The Password is incorrect to the pattern. Password must contain at least 6 characters, including UPPER/lowercase, numbers. Please try again." };
            }
            else
            {
                if (model.Password != model.Re_Password)
                {
                    return new RecoverPasswordResponse { Success = false, Message = "Password do not match. Please check and try again." };
                }
                else
                {
                    
                    return new RecoverPasswordResponse { Success = true, Message= "Your password has been changed. Now you can login." };
                }
            }
            
        }
        
    }
}
