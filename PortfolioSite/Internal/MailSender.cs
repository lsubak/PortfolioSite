using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using Newtonsoft.Json;
using PortfolioSite.Internal.AppSettings;
using PortfolioSite.Models;
using PortfolioSite.Models.Enums;
using System;
using System.Text.RegularExpressions;

namespace PortfolioSite.Internal
{
    public class MailSender
    {
        // Pulled from https://emailregex.com/
        private readonly Regex _emailRegex = new Regex(@"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$");
        private readonly EmailSettings _settings;
        private readonly ILogger<MailSender> _logger;

        public MailSender(IOptions<EmailSettings> options, ILogger<MailSender> logger)
        {
            _settings = options.Value;
            _logger = logger;
        }

        public ContactReturnView SendMail(ContactForm form)
        {
            var match = _emailRegex.Match(form.FromAddress);
            if (match.Success)
            {
                var mimeMessage = new MimeMessage();
                mimeMessage.From.Add(new MailboxAddress(form.FromAddress, _settings.EmailUser));
                mimeMessage.To.Add(new MailboxAddress("Laura", _settings.EmailUser));
                mimeMessage.Subject = $"{form.Name} - {form.Subject}";
                mimeMessage.Body = new TextPart("plain")
                {
                    Text = form.Message
                };

                try
                {
                    using (var client = new SmtpClient())
                    {
                        client.Connect(_settings.MailServerUrl, _settings.MailServerPort, SecureSocketOptions.StartTls);
                        client.Authenticate(_settings.EmailUser, _settings.EmailPassword);
                        client.Send(mimeMessage);
                        client.Disconnect(true);
                        return ContactReturnView.EmailConfirmation;
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError(e.ToString());
                    _logger.LogError(JsonConvert.SerializeObject(form));
                    return ContactReturnView.EmailError;
                }
            }
            else
            {
                return ContactReturnView.EmailInvalidError;
            }
        }
    }
}
