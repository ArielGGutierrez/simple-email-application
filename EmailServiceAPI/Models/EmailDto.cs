using System.ComponentModel.DataAnnotations;

namespace EmailServiceAPI.Models
{
    public class EmailDto
    {
        [Required]
        public string Recipient { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Body { get; set; }
    }
}
