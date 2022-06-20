using CMS_1.Models;
using CMS_1.Models.Campaign;
using CMS_1.Models.Campaigns;

namespace CMS_1.System.Campaign
{
    public interface ICampaignsService
    {
        ICollection<CampaignMV> GetAllCampaigns();
        Task<CreateCampaignResponse> CreateCampaign(CreateCampaignRequest model);
        Task<GenerateNewBarcodeReqsponse> CreateNewBarcodes(GenerateNewBarcodeRequest model);
        Task<GenerateNewBarcodeReqsponse> ChangeStateOfBarcode(int id, bool status);
        ICollection<BarcodeVM> GetAllBarcodesOfCampaign(int id);
        Task<ScanBarcodeResponse> ScanBarcodeForCustomer(int id, string owner);
        ICollection<GiftCampaignMV> GetAllGiftOfCampaign(int id);
        ICollection<RuleOfGiftVM> GetAllRuleOfGiftInCampaign();
        Task<RuleOfGiftResponse> CreateNewRuleOfGift(RuleOfGiftRequest model);
        Task<RuleOfGiftResponse> EditRuleOfGift(RuleOfGiftRequest model, int id);
        Task<RuleOfGiftResponse> ActiveRuleOfGift(bool status, int id);
        Task<RuleOfGiftResponse> RaiseThePriorityOfTheRule(int id);
        Task<RuleOfGiftResponse> ReduceThePriorityOfTheRule(int id);
        Task<RuleOfGiftResponse> DeleteRuleOfGift(int id);
        ICollection<WinnersVM> GetAllWinners();
    }
}
