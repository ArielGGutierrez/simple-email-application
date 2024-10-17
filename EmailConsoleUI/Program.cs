using EmailDLL;
using Microsoft.Extensions.Configuration;

// See https://aka.ms/new-console-template for more information

var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false);
IConfiguration config = configuration.Build();
EmailCredentials credentials = config.GetSection("Email").Get<EmailCredentials>();

bool repeat = true;
Console.WriteLine("Email Service!");
EmailService service = new EmailService();

do
{
    Console.Write("Recipient: ");
    string recipient = Console.ReadLine();

    Console.Write("Subject: ");
    string subject = Console.ReadLine();

    Console.Write("Body: ");
    string body = Console.ReadLine();

    service.SendEmail(credentials, recipient, subject, body);

    Console.WriteLine("Number of Emails Sent: " + service.emailList.Count);

    Console.WriteLine("Repeat?: Y/N");
    string input = Console.ReadLine();

    if (input.ToLower() == "n")
        repeat = false;
} while (repeat == true);