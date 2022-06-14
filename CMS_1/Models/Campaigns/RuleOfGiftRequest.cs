namespace CMS_1.Models.Campaigns
{
    public class RuleOfGiftRequest
    {
        public string RuleName { get; set; }
        public int IdGiftCategory { get; set; }
        public int GiftCount { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool AllDay { get; set; }
        public int Probability { get; set; }
        public int IdRepeatSchedule { get; set; }
        public string ScheduleData { get; set; }

    }
}
