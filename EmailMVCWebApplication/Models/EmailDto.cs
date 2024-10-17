using System.ComponentModel.DataAnnotations;

namespace EmailMVCWebApplication.Models
{
    public class EmailDto
    {
        [Required]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]", ErrorMessage = "Invalid Email Address")]
        public string Recipient { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Body { get; set; }
    }
}