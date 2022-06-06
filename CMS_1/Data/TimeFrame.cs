using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS_1.Models
{
    [Table(name: "TimeFrame")]
    public class TimeFrame
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        public DateTime StartDay { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndDay { get; set; }
        public DateTime EndTime { get; set; }

        public int? IdCampaign { get; set; }
        [ForeignKey("IdCampaign")]
        public Campaign Campaign { get; set; }
    }
}
