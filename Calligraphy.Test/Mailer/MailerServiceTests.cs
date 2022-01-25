using Calligraphy.Mailer.Model;
using Calligraphy.Mailer.Services;
using Calligraphy.Mailer.Settings;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using MimeKit;
using netDumbster.smtp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Calligraphy.Test.Mailer
{
    public class MailerServiceTests
    {
        private readonly MailRequest _request;

        public MailerServiceTests()
        {
            _request = new MailRequest();
            _request.email = "tristanblacklafleur@hotmail.ca";
            _request.subject = "subject1";
            _request.body = "body1";
        }

        //TC6-TS1
        private MimeMessage CreateMailMessage(MailRequest request)
        {
            var email = new MimeMessage();

            email.From.Add(new MailboxAddress("Tristan Lafleur", "codmakeskidsrage@gmail.com"));
            email.To.Add(MailboxAddress.Parse(request.email));
            email.Subject = "netDumpster Test";
            var builder = new BodyBuilder();

            if (request.attachtments != null)
            {
                foreach (var file in request.attachtments.Where(file => file.Length > 0))
                {
                    byte[] fileBytes;
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        fileBytes = ms.ToArray();
                    }

                    builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                }
            }

            builder.HtmlBody = request.body;
            email.Body = builder.ToMessageBody();

            return email;
        }

        //TC6-TS2
        [Fact]
        // Test the server mockup to see if an email can be sent
        public void SendEmailOk()
        {
            // Arrange
            var port = 9009;
            var server = SimpleSmtpServer.Start(port);

            var message = CreateMailMessage(_request);

            // Act
            using (var client = new SmtpClient())
            {
                client.Connect("localhost", port, false);
                client.Send(message);
                client.Disconnect(true);
            }

            var emails = server.ReceivedEmail;
            var mail = emails.First();

            // Assert
            Assert.True(emails.Count() == 1);
            Assert.Equal("netDumpster Test", mail.Subject);
        }
    }
}