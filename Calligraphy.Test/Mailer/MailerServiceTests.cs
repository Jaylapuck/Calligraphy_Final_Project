using Calligraphy.Mailer.Model;
using Calligraphy.Mailer.Services;
using Calligraphy.Mailer.Settings;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Calligraphy.Test.Mailer
{
    public class MailerServiceTests
    {
        private readonly Mock<IMailerService> _service;
        private MailRequest _request;

        public MailerServiceTests()
        {
            _service = new Mock<IMailerService>();
            _request = new MailRequest("tristanblacklafleur@hotmail.ca", "Test", "This is a test.", new List<IFormFile>());
        }

        
    }
}
