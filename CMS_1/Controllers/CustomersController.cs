using CMS_1.Models;
using CMS_1.System.Customers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMS_1.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly ICustomersService _customersService;

        public CustomersController(ICustomersService customersService, AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _customersService = customersService;
        }
        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> GetAll()
        {
            var resul = _customersService.GetAllCustomer();

            return Ok(resul);
        }
        [HttpPut("{id}")]
        //[Authorize]
        public async Task<IActionResult> BlockById(int id, bool block)
        {
            var resul = await _customersService.BlockCustomer(id, block);

            return Ok(resul);
        }
    }
}
