namespace CMS_1.Models.Campaigns
{
    public class BarcodeHistoryVM
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ScannedDate { get; set; }
        public DateTime? SpinDate { get; set; }
        public string Owner { get; set; }
        public bool Scanned { get; set; }
        public bool UseForSpin { get; set; }  
    }
}
