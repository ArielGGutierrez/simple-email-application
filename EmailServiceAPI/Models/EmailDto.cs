using System.ComponentModel.DataAnnotations;

namespace EmailServiceAPI.Models
{
    public class EmailDto
    {
        [Required]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]$", ErrorMessage = "Invalid Email Address")]
        public string Recipient { get; }
        [Required]
        public string Subject { get; }
        [Required]
        public string Body { get; }

        public EmailDto(string recipient, string subject, string body)
        {
            Recipient = recipient;
            Subject = subject;
            Body = body;
        }
    }
}
