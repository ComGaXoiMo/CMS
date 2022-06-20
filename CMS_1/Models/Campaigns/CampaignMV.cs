namespace CMS_1.Models.Campaign
{
    public class CampaignMV
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ActiveCode { get; set; }
        public int  GiftQuantity { get; set; }
        public int Scanned { get; set; }
        public int UseForSpin { get; set; }
        public int Win { get; set; }
    }
}
