namespace CMS_1.Models.GiftCategories
{
    public class GiftCategoriesVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Decription { get; set; }
        public int Count { get; set; }
        public bool Active { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
