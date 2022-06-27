using CMS_1.Models;
using CMS_1.Models.Campaigns;
using CMS_1.Models.Filters;
using CMS_1.Models.GiftCategories;
using Microsoft.EntityFrameworkCore;

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
                    Id = gift.Id,
                    GiftCode = gift.GiftCode,
                    GiftName = gift.GiftName,
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
            var gc = _appDbContext.GiftCategories.SingleOrDefault(x => x.Id == model.IdGiftCategoty);

            for (int i = 0; i < giftstrings.Length; i++)
            {
                giftstrings[i] = characters[random.Next(characters.Length)];
            }
            string str = new string(giftstrings);
            string finalgift = new string("GIF" + str);

            var gift = new Gift
            {
                GiftCode = finalgift,
                GiftName = gc.Name,
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
        public ICollection<GiftCampaignMV> FilterGift(bool MatchAllFilter, List<Condition_Filter> Conditions)
        {
            var str = "";
            for (int i = 0; i < Conditions.Count; i++)
            {

                if (i > 0)
                {
                    if (MatchAllFilter == true)
                    {
                        str = str + "AND ";
                    }
                    else
                    {
                        str = str + "OR ";
                    }
                }
                Condition_Filter condition = Conditions[i];
                str = str + SqlFilterGift(condition.SearchCriteria, condition.Condition, condition.Value);

            }

            var allgift = _appDbContext.Gifts.FromSqlRaw("Select * from dbo.Gift Where " + str).ToList();
            var listgift = new List<GiftCampaignMV>();
            foreach (var g in allgift)
            {
                GiftCampaignMV gift = new GiftCampaignMV
                {
                    Id = g.Id,
                    GiftCode = g.GiftCode,
                    GiftName = g.GiftName,
                    CreateDate = g.CreateDate,
                    Usagelimit = g.UsageLimit,
                    Active = g.Active,
                    Used = g.Used,
                };
                listgift.Add(gift);
            }
            return listgift;
        }
        private string SqlFilterGift(int SearchCriteria, int Condition, string Value)
        {
            var str = "";
            if (SearchCriteria == 1)
            {
                str = str + "GiftCode ";
                if (Condition == 1)
                {
                    return str + "like N'%" + Value + "%' ";
                }
                if (Condition == 2)
                {
                    return "not " + str + "like N'%" + Value + "%' ";
                }
            }
            if (SearchCriteria == 2)
            {
                str = str + "GiftName ";
                if (Condition == 1)
                {
                    return str + "like N'%" + Value + "%' ";
                }
                if (Condition == 2)
                {
                    return "not " + str + "like N'%" + Value + "%' ";
                }
            }
            if (SearchCriteria == 3)
            {
                str = str + "CreateDate ";
                if (Condition == 1)
                {
                    return str + ">= '" + Value + "'";
                }
                if (Condition == 2)
                {
                    return str + "<= '" + Value + "'";
                }
                if (Condition == 3)
                {
                    return str + "= '" + Value + "'";
                }
            }

            if (SearchCriteria == 4)
            {
                str = str + "UsageLimit ";
                if (Condition == 1)
                {
                    return str + ">= '" + Value + "'";
                }
                if (Condition == 2)
                {
                    return str + "<= '" + Value + "'";
                }
                if (Condition == 3)
                {
                    return str + "= '" + Value + "'";
                }
            }

            if (SearchCriteria == 5)
            {
                str = str + "Active ";
                if (Condition == 1)
                {
                    return str + "= '" + Value + "' ";
                }
                if (Condition == 2)
                {
                    return "not " + str + "= '" + Value + "' ";
                }
            }
            return str;
        }

        
    }
}
