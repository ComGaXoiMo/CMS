using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS_1.Models
{
    [Table(name: "GiftCategory")]
    public class GiftCategory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        [Required]
        [StringLength(255)]
        public string Decription { get; set; }
        public int Count { get; set; }
        public bool Active { get; set; }
        public DateTime CreateDate { get; set; }
       // 
        public virtual ICollection<Gift> Gifts { get; set; }
        public virtual ICollection<RuleOfGift> RuleOfGifts { get; set; }
    }
}
