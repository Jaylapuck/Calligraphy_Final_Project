using Calligraphy.Controllers;
using Calligraphy.Mailer.Model;
using Calligraphy.Mailer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
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
        public async Task SendMailOkTestSendEmail()
        {
            // Arrange 
            MailRequest email = new MailRequest();
            email.email = "tristanblacklafleur@hotmail.ca";
            email.subject = "subject1";
            email.body = "body1";
            string filePath = @"..\..\..\Mailer\TestFiles\23784.png";
            using var stream = new MemoryStream(File.ReadAllBytes(filePath).ToArray());
            var formFile = new FormFile(stream, 0, stream.Length, "streamFile", filePath.Split(@"\").Last());
            List<IFormFile> attachtments = new List<IFormFile>();
            attachtments.Add(formFile);
            email.attachtments = attachtments;

            // Simulate the await property in the setup of the post method
            _service.Setup(s => s.SendMailAsync(email)).Returns(async () => { await Task.Yield(); });

            // Act
            var result = await _controller.Send(email);

            // Assert
            Assert.IsType<OkResult>(result);
        }


        [Fact]
        // Test to see that the controller can send an email
        public async Task SendMailOkTestNoSubject()
        {
            // Arrange 
            MailRequest email = new MailRequest();
            email.email = "tristanblacklafleur@hotmail.ca";
            email.body = "body1";
            string filePath = @"..\..\..\Mailer\TestFiles\23784.png";
            using var stream = new MemoryStream(File.ReadAllBytes(filePath).ToArray());
            var formFile = new FormFile(stream, 0, stream.Length, "streamFile", filePath.Split(@"\").Last());
            List<IFormFile> attachtments = new List<IFormFile>();
            attachtments.Add(formFile);
            email.attachtments = attachtments;


            // Simulate the await property in the setup of the post method
            _service.Setup(s => s.SendMailAsync(email)).Returns(async () => { await Task.Yield(); });

            // Act
            var result = await _controller.Send(email);

            // Assert
            Assert.True(string.IsNullOrEmpty(email.subject));
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        // Test to see that the controller can send an email
        public async Task SendMailOkTestNoBody()
        {
            // Arrange 
            MailRequest email = new MailRequest();
            email.email = "tristanblacklafleur@hotmail.ca";
            email.subject = "subject1";
            string filePath = @"..\..\..\Mailer\TestFiles\23784.png";
            using var stream = new MemoryStream(File.ReadAllBytes(filePath).ToArray());
            var formFile = new FormFile(stream, 0, stream.Length, "streamFile", filePath.Split(@"\").Last());
            List<IFormFile> attachtments = new List<IFormFile>();
            attachtments.Add(formFile);
            email.attachtments = attachtments;

            // Simulate the await property in the setup of the post method
            _service.Setup(s => s.SendMailAsync(email)).Returns(async () => { await Task.Yield(); });

            // Act
            var result = await _controller.Send(email);

            // Assert
            Assert.True(string.IsNullOrEmpty(email.body));
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        // Test to have the email sent without any attachments
        public async Task SendMailOkTestNoAttachtments()
        {
            // Arrange 
            MailRequest email = new MailRequest();
            email.email = "tristanblacklafleur@hotmail.ca";
            email.subject = "subject1";
            email.body = "body1";
            email.attachtments = new List<IFormFile>();

            // Simulate the await property in the setup of the post method
            _service.Setup(s => s.SendMailAsync(email)).Returns(async () => { await Task.Yield(); });

            // Act
            var result = await _controller.Send(email);

            // Assert
            Assert.True(email.attachtments.Count == 0);
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        // Test to see that the controller can send an email
        public async Task SendMailBadRequestEmptyObject()
        {
            // Arrange 
            MailRequest email = new MailRequest();

            // Simulate the await property in the setup of the post method
            _service.Setup(s => s.SendMailAsync(email)).Returns(async () => { await Task.Yield(); });

            // Act
            Func<Task> result = () => _controller.Send(email);

            // Assert
            var error = await Assert.ThrowsAsync<Exception>(result);
            Assert.Equal("Email not found", error.Message);
        }

        [Fact]
        // Test to validate that an email must be present
        public async Task SendMailArgumentNullNoEmail()
        {
            // Arrange
            MailRequest email = new MailRequest();
            email.subject = "subject1";
            email.body = "body1";
            string filePath = @"..\..\..\Mailer\TestFiles\23784.png";
            using var stream = new MemoryStream(File.ReadAllBytes(filePath).ToArray());
            var formFile = new FormFile(stream, 0, stream.Length, "streamFile", filePath.Split(@"\").Last());
            List<IFormFile> attachtments = new List<IFormFile>();
            attachtments.Add(formFile);
            email.attachtments = attachtments;

            _service.Setup(s => s.SendMailAsync(email)).Returns(async () => { await Task.Yield(); });

            // Act
            Func<Task> result = () => _controller.Send(email);

            // Assert
            var error = await Assert.ThrowsAsync<Exception>(result);
            Assert.Equal("Email not found", error.Message);
        }

        [Fact]
        // Test to validate that a valid email needs to be received
        public async Task SendMailInvalidInputBadEmail()
        {
            // Arrange
            MailRequest email = new MailRequest();
            email.email = "bademail";
            email.subject = "subject1";
            email.body = "body1";
            string filePath = @"..\..\..\Mailer\TestFiles\23784.png";
            using var stream = new MemoryStream(File.ReadAllBytes(filePath).ToArray());
            var formFile = new FormFile(stream, 0, stream.Length, "streamFile", filePath.Split(@"\").Last());
            List<IFormFile> attachtments = new List<IFormFile>();
            attachtments.Add(formFile);
            email.attachtments = attachtments;

            _service.Setup(s => s.SendMailAsync(email)).Returns(async () => { await Task.Yield(); });

            // Act
            Func<Task> result = () => _controller.Send(email);

            // Assert
            var error = await Assert.ThrowsAsync<Exception>(result);
            Assert.Equal("Not a valid email", error.Message);
        }
    }
}