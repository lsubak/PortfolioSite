using MimeKit;

namespace PortfolioSite.Internal.MailSending
{
    public interface ISmtpClient
    {
        void Send(MimeMessage message);
    }
}
