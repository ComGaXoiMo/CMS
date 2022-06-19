using CMS_1.Models.Customers;

namespace CMS_1.System.Customers
{
    public interface ICustomersService
    {
        ICollection<CustomerVM> GetAllCustomer();
        Task<CustomerResponse> BlockCustomer(int id, bool status);
    }
}
