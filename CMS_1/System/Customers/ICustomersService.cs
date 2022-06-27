using CMS_1.Models.Customers;
using CMS_1.Models.Filters;

namespace CMS_1.System.Customers
{
    public interface ICustomersService
    {
        ICollection<CustomerVM> GetAllCustomer();
        Task<CustomerResponse> BlockCustomer(int id, bool status);
        ICollection<CustomerVM> FilterCustomer(bool MatchAllFilter, List<Condition_Filter> Conditions);
    }
}
