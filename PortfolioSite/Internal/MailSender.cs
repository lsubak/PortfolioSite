using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System;

namespace PortfolioSite.Internal
{
    public class MailSender
    {
        private string _toAddress;
        private string _user;
        private string _password;

        public bool SendMail(string fromAddress, string subject, string message)
        {
            //add input validation

            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress(_toAddress));
            mimeMessage.To.Add(new MailboxAddress(_toAddress));
            mimeMessage.Subject = subject;
            mimeMessage.Body = new TextPart("plain")
            {
                Text = message
            };

            try
            {
                using (var client = new SmtpClient())
                {
                    // for localhost testing, will be removed
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    client.CheckCertificateRevocation = false;

                    client.Connect("smtp.office365.com", 587, SecureSocketOptions.StartTls);
                    client.Authenticate(_user, _password);
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
