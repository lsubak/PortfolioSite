using Microsoft.AspNetCore.Mvc;
using PortfolioSite.Internal.MailSending;
using PortfolioSite.Models;

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
        public IActionResult SubmitContactForm([FromBody]ContactForm form)
        {
            var responseView = _mailSender.SendMail(form);
            return Json(responseView.ToString());
        }

        [Route("Contact/EmailConfirmation")]
        public IActionResult EmailConfirmation()
        {
            return View("EmailConfirmation");
        }

        [Route("Contact/EmailInvalidError")]
        public IActionResult EmailInvalidError()
        {
            return View("EmailInvalidError");
        }

        [Route("Contact/EmailError")]
        public IActionResult EmailError()
        {
            return View("EmailError");
        }
     
    }
}
