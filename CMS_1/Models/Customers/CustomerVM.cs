namespace CMS_1.Models.Customers
{
    public class CustomerVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DoB { get; set; }
        public string Position { get; set; }
        public string TypeOfBusiness { get; set; }
        public string Address { get; set; }
        public bool IsBlock { get; set; }
    }
}
