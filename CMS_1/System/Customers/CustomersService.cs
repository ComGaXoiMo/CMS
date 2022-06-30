using CMS_1.Models;
using CMS_1.Models.Customers;
using CMS_1.Models.Filters;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

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
        public ICollection<CustomerVM> FilterCustomer(bool MatchAllFilter, List<Condition_Filter> Conditions)
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
                str = str + SqlFilterCustomer(condition.SearchCriteria, condition.Condition, condition.Value);

            }

            var allcustomer = _appDbContext.Customers.FromSqlRaw("Select * from dbo.Customer Where " + str).ToList();
            var listCustomer = new List<CustomerVM>();
            foreach (var cus in allcustomer)
            {
                CustomerVM ctm = new CustomerVM
                {
                    Id = cus.Id,
                    Name = cus.Name,
                    PhoneNumber = cus.PhoneNumber,
                    DoB = cus.DoB,
                    Position = cus.Position,
                    TypeOfBusiness = cus.TypeOfBusiness,
                    Address =cus.Address,
                    IsBlock = cus.IsBlock,
                };
                listCustomer.Add(ctm);
            }
            return listCustomer;
        }
        private string SqlFilterCustomer(int SearchCriteria, int Condition, string Value)
        {
            var str = "";
            if (SearchCriteria == 1)
            {
                str = str + "Name ";
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
                str = str + "PhoneNumber ";
                if (Condition == 1)
                {
                    return str + "like N'%" + Value + "%' ";
                }
                if (Condition == 2)
                {
                    return "not " + str + "like N'%" + Value + "%' ";
                }
            }
            if (SearchCriteria == 3)
            {
                str = str + "DoB ";
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


            if (SearchCriteria == 4)
            {
                str = str + "Position ";
                if (Condition == 1)
                {
                    return str + "= N'" + Value + "' ";
                }
                if (Condition == 2)
                {
                    return "not " + str + "= N'" + Value + "' ";
                }
            }

            if (SearchCriteria == 5)
            {
                str = str + "TypeOfBusiness ";
                if (Condition == 1)
                {
                    return str + "= N'" + Value + "' ";
                }
                if (Condition == 2)
                {
                    return "not " + str + "= N'" + Value + "' ";
                }
            }
            if (SearchCriteria == 6)
            {
                str = str + "IsBlock ";
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

        public MemoryStream SpreadsheetCustomer(List<CustomerVM> model)
        {
            var stream = new MemoryStream();


            using (ExcelPackage pck = new ExcelPackage(stream))
            {
                var worksheet = pck.Workbook.Worksheets.Add("Sheet5");
                worksheet.Cells.LoadFromCollection(model, true);
                pck.Save();
            }
            stream.Position = 0;
            return stream;
        }
    }
}
