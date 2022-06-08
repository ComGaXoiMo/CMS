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

        public CampaignController( AppDbContext appDbContext )
        {
           // ICampaignService campaignService,
            _appDbContext = appDbContext;
          //  _campaignService = campaignService;
        }
        // GET: api/<ValuesController>
        [HttpGet]
       // [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var ds = _appDbContext.Campaigns.ToList();
            return Ok(ds);
        }
        [HttpPost]
      //  [Authorize]
        public async Task<IActionResult> Create(Models.Campaign.CreateCampaignRequest model)
        {
            try 
            {
                var campaign = new Campaignn
                {
                    Name = model.Name,
                    AutoUpdate = model.AutoUpdate,
                    JoinOnlyOne = model.JoinOnlyOne,
                    Decription = model.Decription,
                    CodeUsageLimit = model.CodeUsageLimit,
                    Unlimited = model.Unlimited,
                    CountCode = model.CountCode,
                    IdCharset = model.IdCharset,
                    CodeLength = model.CodeLength,
                    Prefix = model.Prefix,
                    Postfix = model.Postfix,
                    IdProgramSize = model.IdProgramSize,

                };
                _appDbContext.Add(campaign);
                _appDbContext.SaveChanges();
                var cp = _appDbContext.Campaigns.SingleOrDefault(c=> c.Name==campaign.Name);
                var tf = new TimeFrame
                {
                    StartDay = model.timeFrame.StartDay.Date,
                    StartTime = model.timeFrame.StartDay.TimeOfDay,
                    EndDay = model.timeFrame.EndDay.Date,
                    EndTime = model.timeFrame.EndDay.TimeOfDay,
                    IdCampaign = cp.Id,
                };
                
                _appDbContext.Add(tf);
                _appDbContext.SaveChanges();
                return Ok(new CreateCampaignResponse { Success = true, Message = "The Campaign (campaign name) is created successfully." });
            }
            catch 
            {
                return BadRequest();
            }
        }

    }
}
