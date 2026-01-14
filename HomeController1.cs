using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MailKit.Net.Smtp;
using EmailSend.Models;
using System.Reflection.Metadata.Ecma335;

namespace EmailSend.Controllers
{
    public class HomeController1 : Controller
    {
        public IActionResult Index()

        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(FormModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Harsh", "sonawaneyashda10@gmail.com"));
            message.To.Add(MailboxAddress.Parse(model.Email));
            message.Subject = "Form Submission";
            message.Body = new TextPart("plain")
            {
                Text = $"Name :{model.Name}\nPhone:{model.Phone}\nMessage:{model.Message}"
            };
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                await client.AuthenticateAsync("sonawaneyashda10@gamil.com", "@Yashiiiii-0263");
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
            return View("Success");

        }
    }
}
