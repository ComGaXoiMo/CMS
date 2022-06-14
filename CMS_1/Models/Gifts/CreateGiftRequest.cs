namespace CMS_1.Models.Campaigns
{
    public class CreateGiftRequest
    {
        public int IdGiftCategoty { get; set; }
        public int IdCampaign { get; set; }
        public string Decription { get; set; }
        public int Giftcount { get; set; }
        public int UseLimit { get; set; }
    }
}
