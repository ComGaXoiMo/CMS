using CMS_1.Models;
using CMS_1.Models.Campaigns;
using CMS_1.Models.GiftCategories;

namespace CMS_1.System.Gifts
{
    public class GiftsService : IGiftsService
    {
        private readonly AppDbContext _appDbContext;

        public GiftsService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<CreateGiftResponse> CreateNewGifts(CreateGiftRequest model)
        {
            try
            {
                for (int i = 0; i < model.Giftcount; i++)
                {
                    AutoCreateGifts(model);
                }
                return new CreateGiftResponse { Success = true, Message = "Generated " + model.Giftcount + " Gift Codes successfully." };
            }
            catch
            {
                return new CreateGiftResponse { Success = false, Message = "Error" };

            }
        }

        public ICollection<GiftsVM> GetAllGifts()
        {
            var gifts = _appDbContext.Gifts.ToList();
            var listgifts = new List<GiftsVM>();
            foreach (var gift in gifts)
            {
                var campaign = _appDbContext.Campaigns.SingleOrDefault(x=>x.Id==gift.IdCampaign);
                GiftsVM giftsVM = new GiftsVM
                {
                    GiftCode = gift.GiftCode,
                    CampaignName = campaign.Name,
                    CreateDate = gift.CreateDate,
                    ExpiredDate = campaign.EndDay,
                    Used = gift.Used,
                    UsageLimit = gift.UsageLimit,
                    Active = gift.Active,
                };
                listgifts.Add(giftsVM);
            }
            return listgifts;
        }
        public void AutoCreateGifts(CreateGiftRequest model)
        {
            var giftstrings = new char[10];
            var random = new Random();
            var characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ123456789";


            for (int i = 0; i < giftstrings.Length; i++)
            {
                giftstrings[i] = characters[random.Next(characters.Length)];
            }
            string str = new string(giftstrings);
            string finalgift = new string("GIF" + str);

            var gift = new Gift
            {
                GiftCode = finalgift,
                CreateDate = DateTime.Now,
                UsageLimit = model.UseLimit,
                Active = true,
                Used = 0,
                IdGiftCategory = model.IdGiftCategoty,
                IdCampaign = model.IdCampaign
            };
            _appDbContext.Add(gift);
            _appDbContext.SaveChanges();
        }
    }
}
