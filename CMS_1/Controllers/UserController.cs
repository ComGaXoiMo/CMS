using CMS_1.Models;
using CMS_1.System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMS_1.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public IActionResult Login(LoginRequest model)
        {
            var result = _userService.Authenticate(model);
            if(result == null)
               
                return BadRequest(new LoginResponse{ Error = "Please check your email and password then try again", Success = false });
            return Ok(result);
        }
    }
}
