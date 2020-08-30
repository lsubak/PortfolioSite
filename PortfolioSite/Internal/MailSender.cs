using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using PortfolioSite.Internal.AppSettings;
using PortfolioSite.Internal.Database;
using PortfolioSite.Models;
using System;

namespace PortfolioSite.Internal
{
    public class MailSender
    {
        private EmailSettings _settings;
        private IDatabaseService _dbService;

        public MailSender(IOptions<EmailSettings> options, IDatabaseService dbService)
        {
            _settings = options.Value;
            _dbService = dbService;
        }

        public bool SendMail(ContactForm form)
        {
            //add input validation
            _dbService.SaveContactMessage(form);

            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress(form.FromAddress, _settings.EmailUser));
            mimeMessage.To.Add(new MailboxAddress(_settings.EmailUser));
            mimeMessage.Subject = $"{form.Name} - {form.Subject}";
            mimeMessage.Body = new TextPart("plain")
            {
                Text = form.Message
            };

            try
            {
                using (var client = new SmtpClient())
                {
                    // for localhost testing, will be removed
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    client.CheckCertificateRevocation = false;

                    client.Connect(_settings.MailServerUrl, _settings.MailServerPort, SecureSocketOptions.StartTls);
                    client.Authenticate(_settings.EmailUser, _settings.EmailPassword);
                    client.Send(mimeMessage);
                    client.Disconnect(true);
                    return true;
                }
            }
            catch (Exception e)
            {
                // try to log message to the database, otherwise, return error to user
            }
            return false;
        }
    }
}
