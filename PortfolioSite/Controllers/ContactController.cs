using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PortfolioSite.Internal;
using PortfolioSite.Internal.AppSettings;
using PortfolioSite.Models;
using PortfolioSite.Models.Enums;

namespace PortfolioSite.Controllers
{
    public class ContactController : Controller
    {
        private MailSender _mailSender;

        public ContactController(IOptions<EmailSettings> options, ILogger<MailSender> logger)
        {
            _mailSender = new MailSender(options, logger);
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
