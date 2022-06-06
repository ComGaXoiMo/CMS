using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS_1.Models
{
    [Table(name: "ValueSchedule")]
    public class ValueSchedule
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(30)]
        public string Value { get; set; }

        public int? IdRepeat { get; set; }
        [ForeignKey("IdRepeat")]
        public RepeatSchedule RepeatSchedule { get; set; }

        public int? IdRule { get; set; }
        [ForeignKey("IdRule")]
        public RuleOfGift RuleOfGift { get; set; }
    }
}
