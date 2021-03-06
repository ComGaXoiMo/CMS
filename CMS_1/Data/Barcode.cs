using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS_1.Models
{
    [Table(name: "Barcode")]
    public class Barcode
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Code { get; set; }
        [Required]
        [StringLength(100)]
        public string BarCode { get; set; }
        [Required]
        [StringLength(100)]
        public string QRcode { get; set; }
        public int CodeUsageLimit { get; set; }
        public bool Unlimited { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ScannedDate { get; set; }
        public DateTime? SpinDate { get; set; }
        [StringLength(30)]
        public string? Owner { get; set; }
        public bool IsScanned { get; set; }
        public bool Active { get; set; }
        public bool UseForSpin { get; set; }
        //public int CodeLength { get; set; }
        //[Required]
        //[StringLength(30)]
        //public string Prefix { get; set; }
        //[Required]
        //[StringLength(30)]
        //public string Postfix { get; set; }
        public int Used { get; set; }
        public int? IdCharset { get; set; }
        [ForeignKey("IdCharset")]
        public Charset Charset { get; set; }
        public int? IdCampaign { get; set; }
        [ForeignKey("IdCampaign")]
        public Campaignn Campaign { get; set; }
    }
}
