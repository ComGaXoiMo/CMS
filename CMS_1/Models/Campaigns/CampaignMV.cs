namespace CMS_1.Models.Campaign
{
    public class CampaignMV
    {
        public string Name { get; set; }

        public bool AutoUpdate { get; set; }
        public bool JoinOnlyOne { get; set; }
        public string Decription { get; set; }
        public int CodeUsageLimit { get; set; }
        public bool Unlimited { get; set; }
        public int CountCode { get; set; }
        public int CodeLength { get; set; }
        public string Prefix { get; set; }
        public string Postfix { get; set; }

        public int? IdCharset { get; set; }
        public int? IdProgramSize { get; set; }

    }
}
