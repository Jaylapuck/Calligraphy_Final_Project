using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Calligraphy.Controllers;
using Calligraphy.Data.Enums;
using Calligraphy.Data.Models;
using Calligraphy.Mailer.Model;
using Calligraphy.Mailer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace Calligraphy.Test.Mailer
{
    public class MailerControllerTests
    {
        private readonly MailController _controller;
        private readonly Mock<IMailerService> _service;
        private readonly Mock<IConfiguration> _mockConfig;

        public MailerControllerTests()
        {
            _service = new Mock<IMailerService>();
            _controller = new MailController(_service.Object, _mockConfig.Object);
        }

        [Fact]
        // Test to see that the controller can send an email
        public async Task SendMailCustomerConfirmationShouldReturnOkResult()
        {
            // Arrange 
            var dummyAddress = new AddressEntity
            {
                AddressId = 1,
                Street = "somne street",
                City = "some city",
                Country = "some country",
                Postal = "some code"
            };
            var dummyCustomer = new CustomerEntity
            {
                CustomerId = 1,
                FirstName = "some name",
                LastName = "some name",
                Address = dummyAddress,
                Email = "tristanblacklafleur@hotmail.ca"
            };
            var filePath = @"..\..\..\Mailer\TestFiles\23784.png";
            using var stream = new MemoryStream((await File.ReadAllBytesAsync(filePath)).ToArray());
            var formFile = new FormFile(stream, 0, stream.Length, "streamFile", filePath.Split(@"\").Last());
            var dummyAttachments = new List<IFormFile> {formFile};
            var dummyQuote = new QuoteEntity
            {
                QuoteId = 1,
                Price = 20,
                Duration = 14,
                Materials = "None",
                ApprovalStatus = Status.Approved
            };
            var dummyForm = new FormEntity
            {
                FormId = 1,
                Customer = dummyCustomer,
                ServiceType = ServiceType.Calligraphy,
                StartingRate = 20.00f,
                CreatedDate = DateTime.Now,
                Comments = "some text",
                Attachments = dummyAttachments,
                Quote = dummyQuote
            };

            // Simulate the await property in the setup of the post method
            _service.Setup(s => s.SendMailAsync(It.IsAny<MailRequest>())).Returns(async () => { await Task.Yield(); });

            // Act
            var result = await _controller.SendCustomerConfirmation(dummyForm);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        // Test to see that the controller can send an email
        public async Task SendMailOwnerAlertNewQuoteShouldReturnOkResult()
        {
            // Arrange 
            var dummyAddress = new AddressEntity
            {
                AddressId = 1,
                Street = "somne street",
                City = "some city",
                Country = "some country",
                Postal = "some code"
            };
            var dummyCustomer = new CustomerEntity
            {
                CustomerId = 1,
                FirstName = "some name",
                LastName = "some name",
                Address = dummyAddress,
                Email = "tristanblacklafleur@hotmail.ca"
            };
            var filePath = @"..\..\..\Mailer\TestFiles\23784.png";
            using var stream = new MemoryStream((await File.ReadAllBytesAsync(filePath)).ToArray());
            var formFile = new FormFile(stream, 0, stream.Length, "streamFile", filePath.Split(@"\").Last());
            var dummyAttachments = new List<IFormFile> {formFile};
            var dummyQuote = new QuoteEntity
            {
                QuoteId = 1,
                Price = 20,
                Duration = 14,
                Materials = "None",
                ApprovalStatus = Status.Approved
            };
            var dummyForm = new FormEntity
            {
                FormId = 1,
                Customer = dummyCustomer,
                ServiceType = ServiceType.Calligraphy,
                StartingRate = 20.00f,
                CreatedDate = DateTime.Now,
                Comments = "some text",
                Attachments = dummyAttachments,
                Quote = dummyQuote
            };

            // Simulate the await property in the setup of the post method
            _service.Setup(s => s.SendMailAsync(It.IsAny<MailRequest>())).Returns(async () => { await Task.Yield(); });

            // Act
            var result = await _controller.SendOwnerAlertNewQuote(dummyForm);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        // Test to see that the controller can send an email
        public async Task SendMailOwnerAlertNewContractShouldReturnOkResult()
        {
            // Arrange 
            var dummyQuote = new QuoteEntity
            {
                QuoteId = 1,
                Price = 200,
                Duration = 10,
                Materials = "Client birthday card",
                ApprovalStatus = Status.Approved
            };
            var dummyCOntract = new ContractEntity
            {
                ContractId = 1,
                FinalCost = 200,
                DownPayment = 100,
                DateCommissioned = DateTime.Now,
                EndDate = DateTime.Now.AddDays(10),
                HasSignature = false,
                IsFinished = false
            };

            // Simulate the await property in the setup of the post method
            _service.Setup(s => s.SendMailAsync(It.IsAny<MailRequest>())).Returns(async () => { await Task.Yield(); });

            // Act
            var result = await _controller.SendOwnerAlertNewContract(dummyQuote, dummyCOntract);

            // Assert
            Assert.IsType<OkResult>(result);
        }
    }
}