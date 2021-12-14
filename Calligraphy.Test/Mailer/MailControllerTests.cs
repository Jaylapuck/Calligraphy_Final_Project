using Calligraphy.Controllers;
using Calligraphy.Mailer.Model;
using Calligraphy.Mailer.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Calligraphy.Test.Mailer
{
    public class MailControllerTests
    {
        private readonly Mock<IMailerService> _service;
        private readonly MailerController _controller;

        public MailControllerTests()
        {
            this._service = new Mock<IMailerService>();
            this._controller = new MailerController(_service.Object);
        }

        [Fact]
        // Test to see that the controller can send an email
        public async Task SendMailOkTest()
        {
            // Arrange 
            MailRequest email = new MailRequest("email1", "subject1", "body1", null);

            _service.Setup(s => s.SendMailAsync(email)).Returns(async () => { await Task.Yield(); });

            // Act
            var result = await _controller.Send(email);

            // Assert
            Assert.IsType<OkResult>(result);
        }
    }
}
