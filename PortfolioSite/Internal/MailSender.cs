using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Util;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System.Threading;

namespace PortfolioSite.Internal
{
    public class MailSender
    {
        private string _toAddress;
        private string _user;
        private string _clientId;
        private string _clientSecret;

        public async void SendMail(string fromAddress, string subject, string message)
        {
            //add input validation

            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress(fromAddress));
            mimeMessage.To.Add(new MailboxAddress(_toAddress));
            mimeMessage.Subject = subject;
            mimeMessage.Body = new TextPart("plain")
            {
                Text = message
            };

            var secrets = new ClientSecrets
            {
                ClientId = _clientId,
                ClientSecret = _clientSecret
            };

            var googleCredentials = await GoogleWebAuthorizationBroker.AuthorizeAsync(secrets, new[] { GmailService.Scope.MailGoogleCom }, _user, CancellationToken.None);
            if (googleCredentials.Token.IsExpired(SystemClock.Default))
            {
                await googleCredentials.RefreshTokenAsync(CancellationToken.None);
            }

            using (var client = new SmtpClient())
            {
                // for localhost testing, will be removed
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                client.CheckCertificateRevocation = false;

                client.Connect("smtp.gmail.com", 465, true);
                var oauth2 = new SaslMechanismOAuth2(googleCredentials.UserId, googleCredentials.Token.AccessToken);
                client.Authenticate(oauth2);
                client.Send(mimeMessage);
                client.Disconnect(true);
            }
        }
    }
}
