using Calligraphy.Mailer.Model;
using Calligraphy.Mailer.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calligraphy.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MailerController : ControllerBase
    {
        private readonly IMailerService _mailerService;

        public MailerController(IMailerService mailerService)
        {
            this._mailerService = mailerService;
        }

        [HttpPost("Send")]
        public async Task<IActionResult> Send([FromForm] MailRequest request)
        {
            await _mailerService.SendMailAsync(request);
            return Ok();
        }
    }
}
