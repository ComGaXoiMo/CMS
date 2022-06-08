using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS_1.Models
{
    [Table(name: "TimeFrame")]
    public class TimeFrame
    {
        [Key]
        public int Id { get; set; }

        public DateTime StartDay { get; set; }
        public TimeSpan StartTime { get; set; }
        public DateTime EndDay { get; set; }
        public TimeSpan EndTime { get; set; }

        public int? IdCampaign { get; set; }
        [ForeignKey("IdCampaign")]
        public Campaignn Campaign { get; set; }
    }
}
