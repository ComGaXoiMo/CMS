using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS_1.Models
{
    [Table(name: "Customer")]
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DoB { get; set; }
        public bool Active { get; set; }
        public string Position { get; set; }
        public string TypeOfBusiness { get; set; }
        public string Address { get; set; }
        public bool IsBlock { get; set; }

        public virtual ICollection<Winner> Winners { get; set; }
    }
}
