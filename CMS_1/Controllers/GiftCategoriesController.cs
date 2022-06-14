using CMS_1.Models;
using CMS_1.Models.GiftCategories;
using CMS_1.System.GiftCategories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMS_1.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GiftCategoriesController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly IGiftCategoriesService _giftCategoriesService;

        public GiftCategoriesController(IGiftCategoriesService giftCategoriesService, AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _giftCategoriesService = giftCategoriesService;
        }

        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> GetAll()
        {
            var resul =  _giftCategoriesService.GetAllGiftCategories();

            return Ok(resul) ;
        }
        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> Create(GiftCategoriesResquest model)
        {
            var resul = await _giftCategoriesService.CreateGiftCategory(model);

            return Ok(resul);
        }
        [HttpPut("{id}")]
        //[Authorize]
        public async Task<IActionResult> ChangeStatusByID(bool status, int id)
        {
            var resul = await _giftCategoriesService.ChangeStatusGiftCategory(id, status);

            return Ok(resul);
        }
        [HttpPut("{id}")]
        //[Authorize]
        public async Task<IActionResult> Edit(GiftCategoriesResquest model, int id)
        {
            var resul = await _giftCategoriesService.EditGiftCategory(model, id);

            return Ok(resul);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            var resul = await _giftCategoriesService.DeleteGiftCategory(id);

            return Ok(resul);
        }

    }
}
