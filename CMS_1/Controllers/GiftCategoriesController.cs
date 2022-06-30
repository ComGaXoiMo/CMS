using CMS_1.Models;
using CMS_1.Models.Filters;
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
        private readonly IGiftCategoriesService _giftCategoriesService;

        public GiftCategoriesController(IGiftCategoriesService giftCategoriesService)
        {
            _giftCategoriesService = giftCategoriesService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var resul =  _giftCategoriesService.GetAllGiftCategories();

            return Ok(resul) ;
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(GiftCategoriesResquest model)
        {
            var resul = await _giftCategoriesService.CreateGiftCategory(model);

            return Ok(resul);
        }
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> ChangeStatusByID([FromBody]bool status, int id)
        {
            var resul = await _giftCategoriesService.ChangeStatusGiftCategory(id, status);

            return Ok(resul);
        }
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Edit(GiftCategoriesResquest model, int id)
        {
            var resul = await _giftCategoriesService.EditGiftCategory(model, id);

            return Ok(resul);
        }
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteById(int id)
        {
            var resul = await _giftCategoriesService.DeleteGiftCategory(id);

            return Ok(resul);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetGiftCategoryFilter(bool MatchAllFilter, List<Condition_Filter> Conditions)
        {
            //Condition_Filter:
            //SearchCriteria: 1: Gift Name
            //  Condition: 1: includes , 2: is not include
            //SearchCriteria: 2: Created Date
            //  Condition: 1: more than, 2: less than, 3: exactly
            //SearchCriteria: 3: Activation status
            //  Condition: 1: is, 2: is not
            try
            {
                var resul = _giftCategoriesService.FilterGiftCategory(MatchAllFilter, Conditions);
                return Ok(resul);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost]
        [Authorize]
        public IActionResult ExportWinnerToExcel(List<GiftCategory> model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            var stream = _giftCategoriesService.SpreadsheetGiftCategory(model);
            stream.Position = 0;
            var excelName = "Gift-Category_list.xlsx";
            var contenType = "application/octet-stream";
            return File(stream, contenType, excelName);
        }
    }
}
