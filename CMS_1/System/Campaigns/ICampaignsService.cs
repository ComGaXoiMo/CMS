using CMS_1.Models;
using CMS_1.Models.Campaign;

namespace CMS_1.System.Campaign
{
    public interface ICampaignsService
    {
        ICollection<Campaignn> GetAllCampaigns();
        Task<CreateCampaignResponse> CreateCampaign(CreateCampaignRequest model);
        Task<GenerateNewBarcodeReqsponse> CreateNewBarcodes(GenerateNewBarcodeRequest model);
        Task<GenerateNewBarcodeReqsponse> ChangeStateOfBarcode(int id, bool status);
        ICollection<BarcodeVM> GetAllBarcodesOfCampaign(int id);
        Task<ScanBarcodeResponse> ScanBarcodeForCustomer(int id, string owner);
    }
}
