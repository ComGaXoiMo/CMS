using CMS_1.Models;
using CMS_1.Models.Campaign;
using CMS_1.Models.Campaigns;
using CMS_1.System.Campaign;
using CMS_1.System.Gifts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CMS_1.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CampaignsController : ControllerBase
    {
        private readonly ICampaignsService _campaignService;
        private readonly IGiftsService _giftsService;

        public CampaignsController(ICampaignsService campaignService, IGiftsService giftsService )
        {
            _campaignService = campaignService;
            _giftsService = giftsService;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var ds =  _campaignService.GetAllCampaigns();
           
            return Ok(ds);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateCampaignRequest model)
        {
                var resul = await _campaignService.CreateCampaign(model);
                return Ok(resul);           
        }
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetAllBarcodesOfCampaign(int id)
        {
            var resul =  _campaignService.GetAllBarcodesOfCampaign(id);
            return Ok(resul);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GenerateNewBarcodes(GenerateNewBarcodeRequest model)
        {
            var resul = await _campaignService.CreateNewBarcodes(model);
            return Ok(resul);
        }
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> ChangeStatusBarcode(int id, [FromBody] bool status)
        {
            var resul = await _campaignService.ChangeStateOfBarcode(id, status);
            return Ok(resul);
        }
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> ScanBarcodeForCustomer( int id, [FromBody] string owner)
        {
            var resul = await _campaignService.ScanBarcodeForCustomer(id, owner);
            return Ok(resul);
        }
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetAllGiftsOfCampaign(int id)
        {
            var resul = _campaignService.GetAllGiftOfCampaign(id);
            return Ok(resul);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GenerateNewGifts(CreateGiftRequest model)
        {
            var resul = await _giftsService.CreateNewGifts(model);
            return Ok(resul);
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllRuleOfGift()
        {
            var resul = _campaignService.GetAllRuleOfGiftInCampaign();
            return Ok(resul);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateRuleOfGift(RuleOfGiftRequest model)
        {
            var resul = await _campaignService.CreateNewRuleOfGift(model);
            return Ok(resul);
        }
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> EditRuleOfGift(RuleOfGiftRequest model,int id)
        {
            var resul = await _campaignService.EditRuleOfGift(model, id);
            return Ok(resul);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> RaiseThePriorityOfTheRule(int id)
        {
            var resul = await _campaignService.RaiseThePriorityOfTheRule(id);
            return Ok(resul);
        }
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> ReducedThePriorityOfTheRule(int id)
        {
            var resul = await _campaignService.ReduceThePriorityOfTheRule(id);
            return Ok(resul);
        }
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteRuleOfGift(int id)
        {
            var resul = await _campaignService.DeleteRuleOfGift(id);
            return Ok(resul);
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllWinners()
        {
            var resul = _campaignService.GetAllWinners();
            return Ok(resul);
        }
    }
}
