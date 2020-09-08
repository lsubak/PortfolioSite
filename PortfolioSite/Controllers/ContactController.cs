using Microsoft.AspNetCore.Mvc;
using PortfolioSite.Internal.MailSending;
using PortfolioSite.Models;
using PortfolioSite.Models.Enums;

namespace PortfolioSite.Controllers
{
    public class ContactController : Controller
    {
        private readonly IMailSender _mailSender;

        public ContactController(IMailSender mailSender)
        {
            _mailSender = mailSender;
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
            switch (_mailSender.SendMail(form))
            {
                case ContactReturnView.EmailConfirmation:
                    return View("EmailConfirmation");
                case ContactReturnView.EmailInvalidError:
                    return View("EmailInvalidError");
                case ContactReturnView.EmailError:
                default:
                    return View("EmailError");
            }
        }
    }
}
