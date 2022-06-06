using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS_1.Models
{
    [Table(name: "RuleOfGift")]
    public class RuleOfGift
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        public int GiftCount { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool AllDay { get; set; }
        public int Probability { get; set; }
        public bool Active { get; set; }
        public int Priority { get; set; }

        public int? IdGiftCategory { get; set; }
        [ForeignKey("IdGiftCategory")]
        public GiftCategory GiftCategory { get; set; }


        public virtual ICollection<ValueSchedule> ValueSchedules { get; set; }

    }
}
