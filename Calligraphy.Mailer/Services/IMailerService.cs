using System.Threading.Tasks;
using Calligraphy.Mailer.Model;

namespace Calligraphy.Mailer.Services
{
    public interface IMailerService
    {
        Task SendMailAsync(MailRequest request);
    }
}