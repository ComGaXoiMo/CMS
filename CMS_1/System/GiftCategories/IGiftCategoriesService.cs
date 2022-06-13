using CMS_1.Models;
using CMS_1.Models.GiftCategories;

namespace CMS_1.System.GiftCategories
{
    public interface IGiftCategoriesService
    {
        ICollection<GiftCategory> GetAllGiftCategories();
        Task<GiftCategoriesResponse> CreateGiftCategory(GiftCategoriesResquest model);
        Task<GiftCategoriesResponse> EditGiftCategory(GiftCategoriesResquest model, int id);
        Task<GiftCategoriesResponse> ChangeStatusGiftCategory(int id, bool status);
        Task<GiftCategoriesResponse> DeleteGiftCategory(int id);
    }
}
