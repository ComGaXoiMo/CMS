using CMS_1.Models;
using CMS_1.Models.Campaign;
using CMS_1.System.Campaign;

namespace CMS_1.System.Campaign
{
    public class CampaignService : ICampaignService
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _appDbContext;

        public CampaignService(IConfiguration configuration, AppDbContext appDbContext)
        {
            _configuration = configuration;
            _appDbContext = appDbContext;
        }

        
    }
}
    