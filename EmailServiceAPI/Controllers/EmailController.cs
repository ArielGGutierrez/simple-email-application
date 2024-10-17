using EmailDLL;
using EmailServiceAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmailServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly EmailService Service;
        private readonly EmailCredentials Credentials;

        public EmailController()
        {
            Service = new EmailService();

            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false);
            IConfiguration config = configuration.Build();
            Credentials = config.GetSection("Email").Get<EmailCredentials>();

        }

        [HttpPost]
        public IActionResult SendEmail(EmailDto request)
        {
            Service.SendEmail(Credentials, request.Recipient, request.Subject, request.Body);
            return Ok();
        }
    }
}
