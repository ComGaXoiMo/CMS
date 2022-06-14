namespace CMS_1.Models.Campaigns
{
    public class RuleOfGiftVM
    {
        public string RuleName { get; set; }
        public string GiftName { get; set; }
        public string Schedule { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool AllDay { get; set; }
        public int TotalGift { get; set; }
        public int Probability { get; set; }
        public bool Active { get; set; }
        public int Priority { get; set; }
        
    }
}
