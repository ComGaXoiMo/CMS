using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS_1.Models
{
    [Table(name: "Campaign")]
    public class Campaignn
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        public bool AutoUpdate { get; set; }
        public bool JoinOnlyOne { get; set; }
        [Required]
        [StringLength(255)]
        public string Decription { get; set; }
        
        public int CountCode { get; set; }
        public DateTime StartDay { get; set; }
        public TimeSpan StartTime { get; set; }
        public DateTime EndDay { get; set; }
        public TimeSpan EndTime { get; set; }
        
        public int? IdProgramSize { get; set; }
        [ForeignKey("IdProgramSize")]
        public ProgramSize ProgramSize { get; set; }

        public virtual ICollection<Barcode> Barcodes { get; set; }
        
        public virtual ICollection<Gift> Gifts { get; set; }

       
    }
}
