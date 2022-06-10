using CMS_1.Models;
using CMS_1.Models.Campaign;

namespace CMS_1.System.Campaign
{
    public interface ICampaignService
    {
        ICollection<Campaignn> GetAllCampaigns();
        Task<CreateCampaignResponse> CreateCampaign(CreateCampaignRequest model);
        void AutoCreateBarCode(int idcampaign, int length, int idcharset, string prefix);
    }
}
