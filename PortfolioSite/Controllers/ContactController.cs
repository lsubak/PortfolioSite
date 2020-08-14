using Microsoft.AspNetCore.Mvc;
using PortfolioSite.Internal;
using PortfolioSite.Models;

namespace PortfolioSite.Controllers
{
    public class ContactController : Controller
    {
        private MailSender _mailSender;

        public ContactController()
        {
            _mailSender = new MailSender();
        }

        [Route("Contact")]
        [Route("Contact/Index")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("Contact/Send")]
        [HttpPost]
        public void SubmitContactForm(ContactForm form)
        {
            _mailSender.SendMail(form.FromAddress, form.Subject, form.Message);
        }
    }
}
