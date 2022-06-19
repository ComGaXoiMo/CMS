using CMS_1.Models;
using CMS_1.Models.Customers;

namespace CMS_1.System.Customers
{
    public class CustomersService : ICustomersService
    {
        private readonly AppDbContext _appDbContext;

        public CustomersService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<CustomerResponse> BlockCustomer(int id, bool status)
        {
            try
            {
                var cus = _appDbContext.Customers.SingleOrDefault(c => c.Id == id);
                if(cus == null)
                {
                    return new CustomerResponse { Success = false, Message = "Don't find id of Customer: '" + id + "' in database." };
                }
                else
                {
                    cus.IsBlock = status;
                    _appDbContext.Update(cus);
                    _appDbContext.SaveChanges();
                    if (cus.IsBlock == true)
                    {
                        return new CustomerResponse { Success = true, Message = "The customer " + cus.Name + " is blocked." };
                    }
                    else
                    {
                        return new CustomerResponse { Success = true, Message = "The customer " + cus.Name + " is unblocked." };
                    }
                }
                
            }
            catch(Exception ex)
            {
                return new CustomerResponse { Success = false, Message = ex.Message };
            }
        }

        public ICollection<CustomerVM> GetAllCustomer()
        {
            var customers = _appDbContext.Customers.ToList();
            var listcustomer = new List<CustomerVM>();
            foreach (var cus in customers)
            {
                CustomerVM cusVM = new CustomerVM
                {
                    Id = cus.Id,
                    Name= cus.Name, 
                    PhoneNumber= cus.PhoneNumber,
                    DoB= cus.DoB,
                    Position= cus.Position,
                    TypeOfBusiness = cus.TypeOfBusiness,
                    Address = cus.Address,
                    IsBlock = cus.IsBlock,
                };
                listcustomer.Add(cusVM);
            }
            return listcustomer;
        }
    }
}
