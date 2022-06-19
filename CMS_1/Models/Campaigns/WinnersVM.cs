namespace CMS_1.Models.Campaigns
{
    public class WinnersVM
    {
        public string FullName { get; set; }
        public DateTime WinDate { get; set; }
        public string GiftCode { get; set; }
        public string GiftName { get; set; }
        public bool SentGift { get; set; }
    }
}
