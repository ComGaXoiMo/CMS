using System.ComponentModel.DataAnnotations;

namespace CMS_1.Models
{
    public class FogotPasswordRequest
    {
        [EmailAddress]
        public string Email { get; set; }
    }
}
