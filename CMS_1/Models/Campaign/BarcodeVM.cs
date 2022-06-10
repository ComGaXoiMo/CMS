namespace CMS_1.Models.Campaign
{
    public class BarcodeVM
    {
        public string Code { get; set; }
        public string BarCode { get; set; }
        public string QRcode { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ScannedDate { get; set; }
        public string Owner { get; set; }
        public bool IsScanned { get; set; }
        public bool Active { get; set; }

        public int? IdCampaign { get; set; }
    }
}
