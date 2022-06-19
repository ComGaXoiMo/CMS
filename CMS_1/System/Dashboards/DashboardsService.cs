using CMS_1.Models;
using CMS_1.Models.Dashboards;

namespace CMS_1.System.Dashboards
{
    public class DashboardsService : IDashboardsService
    {
        private readonly AppDbContext _appDbcontext;

        public DashboardsService (AppDbContext appDbcontext)
        {
            _appDbcontext = appDbcontext;
        }
        public ICollection<DashboardsVM> GetAllCampaigns()
        {
            var listcp = _appDbcontext.Campaigns.ToList();
            List<DashboardsVM> list = new List<DashboardsVM>();
            foreach(var campaign in listcp)
            {
                var dbview = new DashboardsVM
                {
                    IdDashboard = campaign.Id,
                    DashboardName = campaign.Name,
                    StartDate = campaign.StartDay,
                    ExpriedDate = campaign.EndDay,
                    ActiveCode = _appDbcontext.Barcodes.Count(x => x.IdCampaign == campaign.Id),
                    GiftQuantity = _appDbcontext.Gifts.Count(x => x.IdCampaign == campaign.Id),
                    Scanned = _appDbcontext.Barcodes.Where(x => x.IdCampaign == campaign.Id).Count(x => x.IsScanned == true),
                    UseForSpin = 0,
                    Win = 0
                };
                list.Add(dbview);
            }
            return list;
        }
    }
}
