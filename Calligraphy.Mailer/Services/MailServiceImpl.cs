using Calligraphy.Mailer.Model;
using Calligraphy.Mailer.Settings;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Author: Tristan Lafleur
/// 
/// IMailerService implementation class to create an email object from the 
/// smtp settings and request object's fields
/// </summary>

namespace Calligraphy.Mailer.Services
{
    public class MailServiceImpl : IMailerService
    {
        private readonly MailSettings _settings;
        public MailServiceImpl(IOptions<MailSettings> settings)
        {
            this._settings = settings.Value;
        }

        public async Task SendMailAsync(MailRequest request)
        {
            var email = new MimeMessage();

            email.Sender = MailboxAddress.Parse(_settings.mail);
            email.To.Add(MailboxAddress.Parse(request.email));
            email.Subject = request.subject;

            var builder = new BodyBuilder();

            if(request.attachtments != null)
            {
                byte[] fileBytes;
                foreach(var file in request.attachtments)
                {
                    if(file.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            fileBytes = ms.ToArray();
                        }

                        builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                    }
                }
            }

            builder.HtmlBody = request.body;
            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            smtp.Connect(_settings.host, _settings.port, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate(_settings.mail, _settings.password);

            await smtp.SendAsync(email);

            smtp.Disconnect(true);
        }
    }
}
