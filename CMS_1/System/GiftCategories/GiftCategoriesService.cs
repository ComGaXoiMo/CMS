using CMS_1.Models;
using CMS_1.Models.GiftCategories;

namespace CMS_1.System.GiftCategories
{
    public class GiftCategoriesService : IGiftCategoriesService
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _appDbContext;

        public GiftCategoriesService(IConfiguration configuration, AppDbContext appDbContext)
        {
            _configuration = configuration;
            _appDbContext = appDbContext;
        }


        public ICollection<GiftCategory> GetAllGiftCategories()
        {
            var ds = _appDbContext.GiftCategories.ToList();
            return ds;
        }
        public async Task<GiftCategoriesResponse> CreateGiftCategory(GiftCategoriesResquest model)
        {
            try
            {
                var giftcategory = new GiftCategory
                {
                    Name = model.Name,
                    Decription = model.Description,
                    Count = 0,
                    Active = model.Active,
                    CreateDate  = DateTime.Now,
                };
                _appDbContext.GiftCategories.Add(giftcategory);
                _appDbContext.SaveChanges();
                return new GiftCategoriesResponse { success = true, Message = "The Gift "+model.Name+" is added to Gifts Category" };
            }
            catch
            {
                return new GiftCategoriesResponse { success = false, Message = "The Gift information is invalid, please check and try again." };
            }
        }

        public async Task<GiftCategoriesResponse> EditGiftCategory(GiftCategoriesResquest model, int id)
        {
            
            try
            {
                var giftcate = _appDbContext.GiftCategories.SingleOrDefault(x => x.Id == id);
                giftcate.Name = model.Name;
                giftcate.Decription = model.Description;
                _appDbContext.Update(giftcate);
                _appDbContext.SaveChanges();
                return new GiftCategoriesResponse { success = true, Message = "The Gift "+model.Name+" information is updated" };
            }
            catch
            {
                return new GiftCategoriesResponse { success = false, Message = "The Gift information is invalid, please check and try again" };
            }
        }

        public async Task<GiftCategoriesResponse> ChangeStatusGiftCategory(int id, bool status)
        {
            try
            {
                var giftcate = _appDbContext.GiftCategories.SingleOrDefault(x => x.Id == id);
                giftcate.Active = status;
                _appDbContext.Update(giftcate);
                _appDbContext.SaveChanges();
                if (giftcate.Active == true)
                {
                    return new GiftCategoriesResponse { success = true, Message = "The Gift " + giftcate.Name + " is Activated." };
                }
                else
                {
                    return new GiftCategoriesResponse { success = true, Message = "The Gift " + giftcate.Name + " is De-activated." };
                }
            }
            catch
            {
                return new GiftCategoriesResponse { success = false, Message = "Error" };
            }
        }

        public async Task<GiftCategoriesResponse> DeleteGiftCategory(int id)
        {
            try
            {
                var giftcate = _appDbContext.GiftCategories.SingleOrDefault(x=>x.Id==id);
                _appDbContext.Remove(giftcate);
                _appDbContext.SaveChanges();
                return new GiftCategoriesResponse { success = true, Message = "The Gift " + giftcate.Name + " is deleted" };

            }
            catch
            {
                return new GiftCategoriesResponse { success = false, Message = "error" };
            }
        }
    }
}
