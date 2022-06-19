using CMS_1.Models.Campaigns;
using CMS_1.Models;
using CMS_1.System.Gifts;
using Microsoft.AspNetCore.Mvc;
using CMS_1.System.Dashboards;

namespace CMS_1.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DashboardsController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly IDashboardsService _dashboardsService;

        public DashboardsController(IDashboardsService dashboardsService, AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _dashboardsService = dashboardsService;
        }
        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> GetAll()
        {
            var resul = _dashboardsService.GetAllCampaigns();
            return Ok(resul);
        }
        
    }
}
