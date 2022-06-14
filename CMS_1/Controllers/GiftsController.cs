using CMS_1.Models;
using CMS_1.Models.Campaigns;
using CMS_1.System.Gifts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMS_1.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GiftsController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly IGiftsService _giftsService;

        public GiftsController(IGiftsService giftsService, AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _giftsService = giftsService;
        }
        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> GetAll()
        {
            var resul = _giftsService.GetAllGifts();
            return Ok(resul);
        }
        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> CreateNewGifts(CreateGiftRequest model)
        {
            var resul = _giftsService.CreateNewGifts(model);
            return Ok(resul);   
        }
    }
}
