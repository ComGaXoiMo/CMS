using CMS_1.Models;
using CMS_1.Models.Campaign;
using CMS_1.System.Campaign;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CMS_1.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CampaignController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly ICampaignService _campaignService;

        public CampaignController(ICampaignService campaignService, AppDbContext appDbContext )
        {
            _appDbContext = appDbContext;
            _campaignService = campaignService;
        }
        [HttpGet]
       // [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var ds =  _campaignService.GetAllCampaigns();
           
            return Ok(ds);
        }
        [HttpPost]
      //  [Authorize]
        public async Task<IActionResult> Create(Models.Campaign.CreateCampaignRequest model)
        {
                var resul = await _campaignService.CreateCampaign(model);
                return Ok(resul);           
        }
    }
}
