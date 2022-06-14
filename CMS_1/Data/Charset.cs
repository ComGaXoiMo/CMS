using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS_1.Models
{
    [Table(name: "Charset")]
    public class Charset
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        public virtual ICollection<Barcode> Barcodes { get; set; }
    }
}
