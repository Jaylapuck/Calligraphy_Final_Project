using Calligraphy.Mailer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calligraphy.Mailer.Services
{
    public interface IMailerService
    {
        Task SendMailAsync(MailRequest request);
    }
}