using PortfolioSite.Models;
using PortfolioSite.Models.Enums;


namespace PortfolioSite.Internal.MailSending
{
    public interface IMailSender
    {
        public ContactReturnView SendMail(ContactForm form);
    }
}
