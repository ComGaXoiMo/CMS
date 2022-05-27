using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS_1.Models
{
    [Table(name: "User")]
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(20)]
        public string Email { get; set; }   
        [Required]
        [StringLength(20,MinimumLength =6)]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*/d)[a-zA-Z/d]{6,}$")]
        public string Password { get; set; }


    }
}
