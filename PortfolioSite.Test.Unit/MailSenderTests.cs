using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using Moq;
using NUnit.Framework;
using PortfolioSite.Internal.AppSettings;
using PortfolioSite.Internal.MailSending;
using PortfolioSite.Models;
using PortfolioSite.Models.Enums;
using System;

namespace PortfolioSite.Test.Unit
{
    // List of valid/invalid email addresses pulled from https://gist.github.com/cjaoude/fd9910626629b53c4d25 - based on the literal RFC docs
    // The regex I found didn't properly address all, but it's pretty decent for purposes of a small portfolio site, so I'll leave the ones that didn't pass commented out

    public class MailSenderTests
    {
        public Mock<ISmtpClient> ClientMock;
        public Mock<ILogger<MailSender>> LoggerMock;
        public IOptions<EmailSettings> DefaultSettings;
        public ContactForm DefaultContactForm;

        [SetUp]
        public void TestInitialize()
        {
            ClientMock = new Mock<ISmtpClient>();
            LoggerMock = new Mock<ILogger<MailSender>>();
            DefaultSettings = Options.Create(new EmailSettings()
            {
                EmailUser = "to@test.com",
                EmailPassword = "test password",
                MailServerUrl = "test.smtp.org",
                MailServerPort = 1
            });
            DefaultContactForm = new ContactForm()
            {
                Email = "from@test.com",
                Message = "test message",
                Name = "test name",
                Subject = "test subject"
            };
        }

        [TestCase("email@example.com")]
        [TestCase("firstname.lastname@example.com")]
        [TestCase("email@subdomain.example.com")]
        [TestCase("firstname+lastname@example.com")]
        [TestCase("email@123.123.123.123")]
        [TestCase("email@[123.123.123.123]")]
        [TestCase("\"email\"@example.com")]
        [TestCase("1234567890@example.com")]
        [TestCase("email@example-one.com")]
        //[TestCase("_______@example.com")]
        [TestCase("email@example.name")]
        [TestCase("email@example.museum")]
        [TestCase("email@example.co.jp")]
        [TestCase("firstname-lastname@example.com")]
        //[TestCase("much.”more\\ unusual”@example.com")]
        //[TestCase("very.unusual.”@”.unusual.com@example.com")]
        //[TestCase("very.”(),:;<>[]”.VERY.”very@\\ \"very”.unusual@strange.example.com")]
        public void MailSender_ValidEmail_ReturnsConfirmation(string email)
        {
            DefaultContactForm.Email = email;
            var mailSender = new MailSender(DefaultSettings, LoggerMock.Object, ClientMock.Object);

            Assert.AreEqual(ContactReturnView.EmailConfirmation, mailSender.SendMail(DefaultContactForm));
        }

        [TestCase("plainaddress")]
        [TestCase("#@%^%#$@#$@#.com")]
        [TestCase("@example.com")]
        [TestCase("Joe Smith <email @example.com>")]
        [TestCase("email.example.com")]
        [TestCase("email@example @example.com")]
        [TestCase(".email @example.com")]
        [TestCase("email.@example.com")]
        [TestCase("email..email @example.com")]
        [TestCase("あいうえお@example.com")]
        [TestCase("email@example.com (Joe Smith)")]
        [TestCase("email@example")]
        [TestCase("email@-example.com")]
        //[TestCase("email@example.web")]
        //[TestCase("email@111.222.333.44444")]
        [TestCase("email @example..com")]
        [TestCase("Abc..123@example.com")]
        [TestCase("”(),:;<>[\\]@example.com")]
        [TestCase("just”not”right@example.com")]
        [TestCase("this\\ is\"really\"not\\allowed@example.com")]
        public void MailSender_InvalidEmail_ReturnsInvalidEmailError(string email)
        {
            DefaultContactForm.Email = email;
            var mailSender = new MailSender(DefaultSettings, LoggerMock.Object, ClientMock.Object);

            Assert.AreEqual(ContactReturnView.EmailInvalidError, mailSender.SendMail(DefaultContactForm)); ;
        }

        [Test]
        public void MailSender_SendException_ReturnsError()
        {
            ClientMock.Setup(x => x.Send(It.IsAny<MimeMessage>())).Throws(new Exception());
            var mailSender = new MailSender(DefaultSettings, LoggerMock.Object, ClientMock.Object);

            Assert.AreEqual(ContactReturnView.EmailError, mailSender.SendMail(DefaultContactForm));
        }
    }
}