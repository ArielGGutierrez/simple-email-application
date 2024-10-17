using EmailMVCWebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace EmailMVCWebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SendEmail(EmailDto request)
        {
            if (request.Recipient == null || request.Subject == null || request.Body == null
                || !Regex.IsMatch(request.Recipient, @"^[^@\s]+@[^@\s]+\.[^@\s]$"))
            {
                return View("Index");
            }

            string baseUrl = "https://localhost:7073/";

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl + "api/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage postEmail = await client.PostAsJsonAsync("Email", request);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
