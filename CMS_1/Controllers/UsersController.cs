using CMS_1.Models;
using CMS_1.System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMS_1.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
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

            // có token mới lấy được dữ liệu từ claim => mới đổi được mật khẩu
            var claims = HttpContext.User.Claims.Where(x => x.Type == "ID");
            foreach (var claim in claims)
            {
                value = claim.Value; 
            }
          
            var user = await _userService.FindById(Convert.ToInt32(value));
            if (user != null)
            {
                user.Password = model.Password;
                _userService.Update(user);
                return Ok(resul);
            }
            else
            {
                return BadRequest();
            }          
        }
    }
}
