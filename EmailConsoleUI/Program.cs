using EmailDLL;
using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;

EmailCredentials credentials = ImportCredentials();

Console.WriteLine("Is this the correct email account?: " + credentials.Address);
Console.Write("y/n: ");

if (!YesOrNo()) // Change Email From
{
    Console.Write("New Email: ");
    credentials.Address = Console.ReadLine();

    Console.Write("Password: ");
    credentials.Password = Console.ReadLine();

    credentials.Host = "smtp." + credentials.Address.Split('@')[1];
}

/* Create Email Service */
Console.WriteLine("Email Service!");
EmailService service = new EmailService();

do
{
    string recipient = "", subject = "", body = "", tryAgain = "";

    Console.Write("Recipient: ");
    do
    {
        recipient = Console.ReadLine();
    }
    while (recipient == "" || !Regex.IsMatch(recipient, @"^[^@\s]+@[^@\s]+\.[^@\s]"));

    Console.Write("Subject: ");
    do
    {
        subject = Console.ReadLine();
    }
    while (subject == "");

    Console.Write("Body: ");
    do
    {
        body = Console.ReadLine();
    }
    while (body == "");

    service.SendEmail(credentials, recipient, subject, body);

    Console.WriteLine("Number of Emails Sent: " + service.EmailList.Count);

    Console.WriteLine("Repeat?: y/n");
} while (YesOrNo());

bool YesOrNo()
{
    string input = "";
    do
    {
        input = Console.ReadLine().ToLower();
    } while (input == "" || (input != "y" && input != "n"));

    return input == "y";
}

EmailCredentials ImportCredentials()
{
    var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false);
    IConfiguration config = configuration.Build();
    return config.GetSection("Email").Get<EmailCredentials>();
}

