namespace CMS_1.Models.Dashboards
{
    public class DashboardsVM
    {
        public int IdDashboard { get; set; }
        public string DashboardName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpriedDate { get; set; }
        public int ActiveCode { get; set; }
        public int GiftQuantity { get; set; }
        public int Scanned { get; set; }
        public int UseForSpin { get; set; }
        public int Win { get; set; }
    }
}
