using CMS_1.Models.Dashboards;

namespace CMS_1.System.Dashboards
{
    public interface IDashboardsService
    {
        ICollection<DashboardsVM> GetAllCampaigns();
    }
}
