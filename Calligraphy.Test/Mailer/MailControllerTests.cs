﻿using Calligraphy.Controllers;
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

/// <summary>
/// Author: Tristan Lafleur
/// 
/// Test class to test the controller for Mailer and it's post method
/// Uses xUnit and Moq as testing libraries
/// 
/// </summary>

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
            MailRequest email = new MailRequest();

            // Simulate the await property in the setup of the post method
            _service.Setup(s => s.SendMailAsync(email)).Returns(async () => { await Task.Yield(); });

            // Act
            var result = await _controller.Send(email);

            // Assert
            Assert.IsType<OkResult>(result);
        }
    }
}