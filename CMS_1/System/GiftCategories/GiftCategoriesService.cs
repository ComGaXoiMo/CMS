using CMS_1.Models;
using CMS_1.Models.Filters;
using CMS_1.Models.GiftCategories;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

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


        public ICollection<GiftCategoriesVM> GetAllGiftCategories()
        {
            var allgiftcategory = _appDbContext.GiftCategories.ToList();
            var listgiftcategory = new List<GiftCategoriesVM>();
            foreach (var giftCategory in allgiftcategory)
            {
                GiftCategoriesVM gc = new GiftCategoriesVM
                {
                    Id = giftCategory.Id,
                    Name = giftCategory.Name,
                    Decription = giftCategory.Decription,
                    Count = giftCategory.Count,
                    CreateDate = giftCategory.CreateDate,
                    Active = giftCategory.Active,
                };
                listgiftcategory.Add(gc);
            }
            return listgiftcategory;
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
                if(giftcate == null)
                {
                    return new GiftCategoriesResponse { success = false, Message = "Don't find this Gift" };
                }
                else
                {
                    giftcate.Name = model.Name;
                    giftcate.Decription = model.Description;
                    giftcate.Active = model.Active;
                    _appDbContext.Update(giftcate);
                    _appDbContext.SaveChanges();
                    return new GiftCategoriesResponse { success = true, Message = "The Gift " + model.Name + " information is updated" };

                }
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
                if (giftcate == null )
                {
                    return new GiftCategoriesResponse { success = false, Message = "don't find this gift." };
                }
                else
                {
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

        public ICollection<GiftCategoriesVM> FilterGiftCategory(bool MatchAllFilter, List<Condition_Filter> Conditions)
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
                str = str + SqlFilterGiftCategory(condition.SearchCriteria, condition.Condition, condition.Value);

            }

            var allgiftcategory = _appDbContext.GiftCategories.FromSqlRaw("Select * from dbo.GiftCategory Where " + str).ToList();
            var listgiftcategory = new List<GiftCategoriesVM>();
            foreach (var giftCategory in allgiftcategory)
            {
                GiftCategoriesVM gc = new GiftCategoriesVM
                {
                    Id = giftCategory.Id,
                    Name = giftCategory.Name,
                    Decription = giftCategory.Decription,
                    Count = giftCategory.Count,
                    CreateDate = giftCategory.CreateDate,
                    Active = giftCategory.Active,
                };
                listgiftcategory.Add(gc);
            }
            return listgiftcategory;
        }
        private string SqlFilterGiftCategory(int SearchCriteria, int Condition, string Value)
        {
            var str = "";
            if (SearchCriteria == 1)
            {
                str = str + "name ";
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
            if (SearchCriteria == 3)
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

        public MemoryStream SpreadsheetGiftCategory(List<GiftCategoriesVM> model)
        {
            var stream = new MemoryStream();


            using (ExcelPackage pck = new ExcelPackage(stream))
            {
                var worksheet = pck.Workbook.Worksheets.Add("Sheet7");
                worksheet.Cells.LoadFromCollection(model, true);
                pck.Save();
            }
            stream.Position = 0;
            return stream;
        }
    }
}
