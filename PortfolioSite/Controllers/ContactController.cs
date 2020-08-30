using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PortfolioSite.Internal;
using PortfolioSite.Internal.AppSettings;
using PortfolioSite.Models;
using System;

namespace PortfolioSite.Controllers
{
    public class ContactController : Controller
    {
        private MailSender _mailSender;

        public ContactController(IOptions<EmailSettings> options)
        {
            _mailSender = new MailSender(options);
        }

        [Route("Contact")]
        [Route("Contact/Index")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("Contact/Send")]
        [HttpPost]
        public IActionResult SubmitContactForm(ContactForm form)
        {
            if (_mailSender.SendMail(form))
            {
                return View("EmailConfirmation");
            }
            return View("EmailError");
        }
    }
}
