namespace CMS_1.Models.Campaign
{
    public class CreateBarcodeByCampaign
    {
      //  public int IdCampaign { get; set; }
        public int CodeUsageLimit { get; set; }
        public bool Unlimited { get; set; }
    //    public int CountCode { get; set; }
        public int CodeLength { get; set; }
        public string Prefix { get; set; }
        public string Postfix { get; set; }

        public int IdCharset { get; set; }
    }
}
