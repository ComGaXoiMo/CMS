using CMS_1.Models;
using CMS_1.Models.Filters;
using CMS_1.System.Customers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMS_1.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomersService _customersService;

        public CustomersController(ICustomersService customersService)
        {
            _customersService = customersService;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var resul = _customersService.GetAllCustomer();

            return Ok(resul);
        }
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> BlockById( int id, [FromBody] bool block)
        {
            var resul = await _customersService.BlockCustomer(id, block);

            return Ok(resul);
        }
        [HttpPost]
        //  [Authorize]
        public async Task<IActionResult> GetCustomerFilter(bool MatchAllFilter, List<Condition_Filter> Conditions)
        {
            //Condition_Filter:
            //SearchCriteria: 1 : Name , 2: Phone Number
            //  Condition: 1: includes , 2: is not include
            //SearchCriteria: 3: Dob 
            //  Condition: 1: more than, 2: less than, 3: exactly
            //SearchCriteria: 4: Position, 5: Type of Business, 6: Status
            //  Condition: 1: is, 2: is not
            try
            {
                var resul = _customersService.FilterCustomer(MatchAllFilter, Conditions);
                return Ok(resul);
            }
            catch
            {
                return BadRequest();
            }

        }
    }
}
