using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS_1.Models
{
    [Table(name: "Gift")]
    public class Gift
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string GiftCode { get; set; }
        public DateTime CreateDate { get; set; }
        public bool Usage { get; set; }
        public bool Active { get; set; }

        public int? IdGiftCategory { get; set; }
        [ForeignKey("IdGiftCategory")]
        public GiftCategory GiftCategory { get; set; }
        public int? IdCampaign { get; set; }
        [ForeignKey("IdCampaign")]
        public Campaign Campaign { get; set; }

        public virtual ICollection<Winner> Winners { get; set; }
    }
}
