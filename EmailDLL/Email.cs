using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailDLL
{
    public class Email
    {
        public enum EmailStatus
        {
            Queued,
            Sending,
            Sent,
            Failed_To_Send
        }

        public string Sender { get; }
        public string Recipient { get; }
        public string Subject { get; }
        public string Body { get; }

        public EmailStatus Status { get; set; }
        public DateTime DateCreated { get; }

        public Email(string sender, string recipient, string subject, string body)
        {
            Sender = sender;
            Recipient = recipient;
            Subject = subject;
            Body = body;

            Status = EmailStatus.Queued;
            DateCreated = DateTime.Now;
        }
    }
}
