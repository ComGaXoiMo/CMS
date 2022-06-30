using CMS_1.Models.Campaigns;
using CMS_1.Models.Filters;
using CMS_1.Models.GiftCategories;

namespace CMS_1.System.Gifts
{
    public interface IGiftsService
    {
        ICollection<GiftsVM> GetAllGifts();
        Task<CreateGiftResponse> CreateNewGifts(CreateGiftRequest model);
        ICollection<GiftCampaignMV> FilterGift(bool MatchAllFilter, List<Condition_Filter> Conditions);

        MemoryStream SpreadsheetGift(List<GiftsVM> model);
    }
}
