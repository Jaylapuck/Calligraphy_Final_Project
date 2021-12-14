using Calligraphy.Mailer.Model;
using Calligraphy.Mailer.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calligraphy.Controllers
{
    public class MailerController : ControllerBase
    {
        private readonly IMailerService _mailerService;

        public MailerController(IMailerService mailerService)
        {
            this._mailerService = mailerService;
        }

        public async Task<IActionResult> Send([FromForm] MailRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
