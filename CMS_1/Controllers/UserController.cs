using CMS_1.Models;
using CMS_1.System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMS_1.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest model)
        {
            var result = await _userService.Authenticate(model);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult>  ForgotPassword(FogotPasswordRequest model)
        {
            
            var result = await _userService.ForgotPassword(model);          
            return Ok(result);           
        }
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> RecoverPassword(RecoverPasswordRequest model)
        {
            var resul = await _userService.RecoverPassword(model);
            return Ok(resul);
        }
    }
}
