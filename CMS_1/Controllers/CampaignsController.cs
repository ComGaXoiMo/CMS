using CMS_1.Models;
using CMS_1.Models.Campaign;
using CMS_1.Models.Campaigns;
using CMS_1.System.Campaign;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CMS_1.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CampaignsController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly ICampaignsService _campaignService;

        public CampaignsController(ICampaignsService campaignService, AppDbContext appDbContext )
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
        public async Task<IActionResult> Create(CreateCampaignRequest model)
        {
                var resul = await _campaignService.CreateCampaign(model);
                return Ok(resul);           
        }
        [HttpGet("{id}")]
        //  [authorize]
        public async Task<IActionResult> GetAllBarcodesOfCampaign(int id)
        {
            var resul =  _campaignService.GetAllBarcodesOfCampaign(id);
            return Ok(resul);
        }
        [HttpPost]
        //  [authorize]
        public async Task<IActionResult> GenerateNewBarcodes(GenerateNewBarcodeRequest model)
        {
            var resul = await _campaignService.CreateNewBarcodes(model);
            return Ok(resul);
        }
        [HttpPut("{id}")]
        //  [authorize]
        public async Task<IActionResult> ChangeStatusBarcode(int id, bool status)
        {
            var resul = await _campaignService.ChangeStateOfBarcode(id, status);
            return Ok(resul);
        }
        [HttpPut("{id}")]
        //  [authorize]
        public async Task<IActionResult> ScanBarcodeForCustomer(int id, string owner)
        {
            var resul = await _campaignService.ScanBarcodeForCustomer(id, owner);
            return Ok(resul);
        }
        [HttpGet("{id}")]
        //  [authorize]
        public async Task<IActionResult> GetAllGiftsOfCampaign(int id)
        {
            var resul = _campaignService.GetAllGiftOfCampaign(id);
            return Ok(resul);
        }
        [HttpPost]
        //  [authorize]
        public async Task<IActionResult> GenerateNewGifts(CreateGiftRequest model)
        {
            var resul = await _campaignService.CreateNewGifts(model);
            return Ok(resul);
        }
    }
}
