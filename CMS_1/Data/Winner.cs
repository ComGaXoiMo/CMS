using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS_1.Models
{
    [Table(name: "Winner")]
    public class Winner
    {
        [Key]
        public int Id { get; set; }
        public DateTime WinDate { get; set; }
        public bool SendGiftStatus { get; set; }

        public int? IdCustomer { get; set; }
        [ForeignKey("IdCustomer")]
        public Customer Customer { get; set; }
        public int? IdGift { get; set; }
        [ForeignKey("IdGift")]
        public Gift Gift { get; set; }


    }
}
