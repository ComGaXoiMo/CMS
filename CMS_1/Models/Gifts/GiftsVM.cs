namespace CMS_1.Models.GiftCategories
{
    public class GiftsVM
    {
        public int Id { get; set; }
        public string GiftCode { get; set; }
        public string GiftName { get; set; }
        public string CampaignName { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ExpiredDate { get; set; }
        public int Used { get; set; }
        public int UsageLimit { get; set; }
        public bool Active { get; set; }
    }
}
