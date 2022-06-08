using CMS_1.Models;
using CMS_1.System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMS_1.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly AppDbContext _appDbContext;

        public UserController(IUserService userService, AppDbContext appDbContext)
        {
            _userService = userService;
            _appDbContext = appDbContext;
        }

        [HttpPost]
        public async Task<IActionResult> Login( LoginRequest model)
        {
            var result = await _userService.Authenticate(model);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult>  ForgotPassword( FogotPasswordRequest model)
        {
            var result = await _userService.ForgotPassword(model);          
            return Ok(result);           
        }
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> RecoverPassword(RecoverPasswordRequest model)
        {
            var resul = await _userService.RecoverPassword(model);            
            var value = "";
            var claims = HttpContext.User.Claims.Where(x => x.Type == "ID");
            foreach (var claim in claims)
            {
                value = claim.Value; 
            }
            var user = _appDbContext.Users.SingleOrDefault(u => u.Id == Convert.ToInt32(value));

            if(user != null)
            {
                user.Password = model.Password;
                _appDbContext.Update(user);
                _appDbContext.SaveChanges();
                return Ok(resul);
            }
            else
            {
                return BadRequest();
            }          
        }
    }
}
