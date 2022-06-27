using CMS_1.Models;
using CMS_1.Models.Campaigns;
using CMS_1.Models.Filters;
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
        private readonly IGiftsService _giftsService;

        public GiftsController(IGiftsService giftsService)
        {
            _giftsService = giftsService;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var resul =  _giftsService.GetAllGifts();
            return Ok(resul);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateNewGifts(CreateGiftRequest model)
        {
            var resul = _giftsService.CreateNewGifts(model);
            return Ok(resul);   
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetGiftFilter(bool MatchAllFilter, List<Condition_Filter> Conditions)
        {
            //Condition_Filter:
            //SearchCriteria: 1 : Code
            //  Condition: 1: includes , 2: is not include
            //SearchCriteria: 2: Created Date, 3: Expired Date, 4: Scanned date
            //  Condition: 1: more than, 2: less than, 3: exactly
            //SearchCriteria: 5: Scanned Status, 6: Activation Status
            //  Condition: 1: is, 2: is not
            try
            {
                var resul = _giftsService.FilterGift(MatchAllFilter, Conditions);
                return Ok(resul);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
