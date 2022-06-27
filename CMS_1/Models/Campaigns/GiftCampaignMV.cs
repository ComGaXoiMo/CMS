namespace CMS_1.Models.Campaigns
{
    public class GiftCampaignMV
    {
        public int Id { get; set; }
        public string GiftCode { get; set; }
        public string GiftName { get; set; }
        public DateTime CreateDate { get; set; }
        public int Usagelimit { get; set; }
        public bool Active { get; set; }
        public int Used { get; set; }
    }
}
