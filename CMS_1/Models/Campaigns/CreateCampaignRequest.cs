namespace CMS_1.Models.Campaign
{
    public class CreateCampaignRequest
    {
        public string Name { get; set; }

        public bool AutoUpdate { get; set; }
        public bool JoinOnlyOne { get; set; }
        public string Decription { get; set; }
        
        public int CountCode { get; set; }
        public CreateBarcodeByCampaign CreateBarcodeRequest { get; set; }
        public int IdProgramSize { get; set; }
        public TimeFrameMV timeFrame { get; set; }    
    }
}
