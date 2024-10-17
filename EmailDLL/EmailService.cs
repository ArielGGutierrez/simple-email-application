using System.ComponentModel;
using MailKit.Net.Smtp;
using MimeKit;
using System.Threading;
using MimeKit.Tnef;
using System.Text.Json;

namespace EmailDLL
{
    public class EmailService
    {
        public List<Email> emailList;
        JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };
        public EmailService()
        {
            emailList = new List<Email>();

            if (File.Exists("EmailList.json"))
            {
                emailList = JsonSerializer.Deserialize<List<Email>>(File.ReadAllText("EmailList.json"));
            }
            else
            {
                emailList = new List<Email>();
            }
        }

        public void SendEmail(EmailCredentials credentials, string recipient, string subject, string body)
        {
            if (recipient == "" || subject == "" || body == "") return;

            Email email = new Email(credentials.Address, recipient, subject, body);
            emailList.Add(email);
            SaveEmailList();

            EmailWorker worker = new EmailWorker(this, email, credentials);
            Thread workerThread = new Thread(new ThreadStart(worker.RunWorker));
            workerThread.Start();
        }

        private void SaveEmailList()
        {
            File.WriteAllText("EmailList.json", JsonSerializer.Serialize(emailList, options));
        }

        private class EmailWorker
        {
            private EmailService Parent;
            private Email Message;
            private EmailCredentials Credentials;

            public EmailWorker(EmailService parent, Email message, EmailCredentials credentials)
            {
                Parent = parent;
                Message = message;
                Credentials = credentials;
            }

            public void RunWorker()
            {
                MimeMessage mimeMessage = new MimeMessage();
                mimeMessage.From.Add(MailboxAddress.Parse(Message.Sender));
                mimeMessage.To.Add(MailboxAddress.Parse(Message.Recipient));
                mimeMessage.Subject = Message.Subject;
                mimeMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = Message.Body };

                Message.Status = Email.EmailStatus.Sending;
                Parent.SaveEmailList();
                bool success = false;

                using SmtpClient client = new SmtpClient();
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if (SendEmail(client, mimeMessage))
                        {
                            success = true;
                            break;
                        }
                    }

                    if (client.IsConnected) client.Disconnect(true);
                }

                if (success)
                    Message.Status = Email.EmailStatus.Sent;
                else
                    Message.Status = Email.EmailStatus.Failed_To_Send;
                
                Parent.SaveEmailList();
            }

            private bool SendEmail(SmtpClient client, MimeMessage message)
            {
                try
                {
                    if (!client.IsConnected)     client.Connect(Credentials.Host, 587, MailKit.Security.SecureSocketOptions.StartTls);
                    if (!client.IsAuthenticated) client.Authenticate(Credentials.Address, Credentials.Password);

                    client.Send(message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }

                return true;
            }
        }
    }
}
