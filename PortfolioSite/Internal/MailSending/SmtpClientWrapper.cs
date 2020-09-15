using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using PortfolioSite.Internal.AppSettings;

namespace PortfolioSite.Internal.MailSending
{
    public class SmtpClientWrapper : ISmtpClient
    {
        private readonly EmailSettings _settings;

        public SmtpClientWrapper(IOptions<EmailSettings> options)
        {
            _settings = options.Value;
        }

        public void Send(MimeMessage mimeMessage)
        {
            using (var client = new SmtpClient())
            {
                client.Connect(_settings.MailServerUrl, _settings.MailServerPort, SecureSocketOptions.StartTls);
                client.Authenticate(_settings.EmailUser, _settings.EmailPassword);
                client.Send(mimeMessage);
                client.Disconnect(true);
            }
        }
    }
}
