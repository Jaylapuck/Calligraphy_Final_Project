using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Calligraphy.Business.Form;
using Calligraphy.Controllers;
using Calligraphy.Data.Enums;
using Calligraphy.Data.Models;
using Calligraphy.Data.Pagination;
using Calligraphy.Mailer.Model;
using Calligraphy.Mailer.Services;
using HttpContextMoq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using Xunit;

namespace Calligraphy.Test.Form
{
    public class FormControllerTests
    {
        private readonly FormController _formController;

        //logger
        private readonly ILogger<FormController> _logger;
        private readonly Mock<IFormService> _mockFormService;
        private readonly Mock<IMailerService> _mockMailerService;

        public FormControllerTests()
        {
            _logger = Mock.Of<ILogger<FormController>>();
            _mockFormService = new Mock<IFormService>();
            _mockMailerService = new Mock<IMailerService>();
            _formController = new FormController(_mockFormService.Object, _mockMailerService.Object, _logger);
        }

        //TC4-TC1
        [Fact]
        public void GetAllServicesOk()
        {
            // Arrange
            var dummyServices = new List<ServiceEntity>
            {
                new() {ServiceId = 1, TypeName = ServiceType.Calligraphy, StartingRate = 20.00f},
                new() {ServiceId = 2, TypeName = ServiceType.Engraving, StartingRate = 30.00f}
            };

            _mockFormService.Setup(x => x.GetAllServices()).Returns(dummyServices);

            // Act
            var result = _formController.GetServices();

            // Assert
            Assert.IsType<List<ServiceEntity>>(result);
            Assert.Equal(2, dummyServices.Count);
        }

        //TC4-TC2
        [Fact]
        public void GetAllServicesEmpty()
        {
            // Arrange
            var dummyServices = new List<ServiceEntity>();

            _mockFormService.Setup(x => x.GetAllServices()).Returns(dummyServices);

            // Act
            var result = _formController.GetServices();

            // Assert
            Assert.IsType<List<ServiceEntity>>(result);
            Assert.Empty(dummyServices);
        }

        //TC4-TC3
        [Fact]
        // Test to see if we get a successful post
        public async void PostOkResultTest()
        {
            // Arrange
            var dummyAddress = new AddressEntity
            {
                AddressId = 1, Street = "somne street", City = "some city", Country = "some country",
                Postal = "some code"
            };
            var dummyCustomer = new CustomerEntity
            {
                CustomerId = 1, FirstName = "some name", LastName = "some name", Address = dummyAddress,
                Email = "tristanblacklafleur@hotmail.ca"
            };
            var filePath = @"..\..\..\Mailer\TestFiles\23784.png";
            using var stream = new MemoryStream((await File.ReadAllBytesAsync(filePath)).ToArray());
            var formFile = new FormFile(stream, 0, stream.Length, "streamFile", filePath.Split(@"\").Last());
            var dummyAttachments = new List<IFormFile> {formFile};
            var dummyForm = new FormEntity
            {
                FormId = 1, Customer = dummyCustomer, ServiceType = ServiceType.Calligraphy, StartingRate = 20.00f,
                Comments = "some text", Attachments = dummyAttachments
            };
            var dummyRequest = new MailRequest();

            _mockFormService.Setup(x => x.Create(dummyForm)).Returns(true);
            _mockMailerService.Setup(s => s.SendMailAsync(dummyRequest)).Returns(async () => { await Task.Yield(); });

            // Act
            var actual = await _formController.Post(dummyForm);

            // Assert
            Assert.IsType<OkObjectResult>(actual);
        }

        //TC4-TC4
        [Fact]
        // Test to see if we get a failed post
        public async void PostBadRequestTest()
        {
            // Arrange
            var dummyAddress = new AddressEntity
            {
                AddressId = 1, Street = "somne street", City = "some city", Country = "some country",
                Postal = "some code"
            };
            var dummyCustomer = new CustomerEntity
            {
                CustomerId = 1, FirstName = "some name", LastName = "some name", Address = dummyAddress,
                Email = "tristanblacklafleur@hotmail.ca"
            };
            const string filePath = @"..\..\..\Mailer\TestFiles\23784.png";
            await using var stream = new MemoryStream((await File.ReadAllBytesAsync(filePath)).ToArray());
            var formFile = new FormFile(stream, 0, stream.Length, "streamFile", filePath.Split(@"\").Last());
            var dummyAttachments = new List<IFormFile> {formFile};
            var dummyForm = new FormEntity
            {
                FormId = 1, Customer = dummyCustomer, ServiceType = ServiceType.Calligraphy, StartingRate = 20.00f,
                Comments = "some text", Attachments = dummyAttachments
            };

            _mockFormService.Setup(x => x.Create(dummyForm)).Returns(false);

            // Act
            var actual = await _formController.Post(dummyForm);

            // Assert
            Assert.IsType<BadRequestResult>(actual);
        }

        //TC4-TC5
        [Fact]
        // Test to see if we get a successful post wo/an attachment
        public async void PostOkResultTestNoAttachments()
        {
            // Arrange
            var dummyAddress = new AddressEntity
            {
                AddressId = 1, Street = "somne street", City = "some city", Country = "some country",
                Postal = "some code"
            };
            var dummyCustomer = new CustomerEntity
            {
                CustomerId = 1, FirstName = "some name", LastName = "some name", Address = dummyAddress,
                Email = "tristanblacklafleur@hotmail.ca"
            };
            var dummyForm = new FormEntity
            {
                FormId = 1, Customer = dummyCustomer, ServiceType = ServiceType.Calligraphy, StartingRate = 20.00f,
                Comments = "some text", Attachments = new List<IFormFile>()
            };
            var dummyRequest = new MailRequest();

            _mockFormService.Setup(x => x.Create(dummyForm)).Returns(true);
            _mockMailerService.Setup(s => s.SendMailAsync(dummyRequest)).Returns(async () => { await Task.Yield(); });

            // Act
            var actual = await _formController.Post(dummyForm);

            // Assert
            Assert.Empty(dummyForm.Attachments);
            Assert.IsType<OkObjectResult>(actual);
        }

        //TC4-TC6
        [Fact]
        // Test to see if we get an exception when encountering an empty email
        public async void PostBadRequestTestNoEmail()
        {
            // Arrange
            var dummyAddress = new AddressEntity
            {
                AddressId = 1, Street = "somne street", City = "some city", Country = "some country",
                Postal = "some code"
            };
            var dummyCustomer = new CustomerEntity
                {CustomerId = 1, FirstName = "some name", LastName = "some name", Address = dummyAddress, Email = ""};
            var filePath = @"..\..\..\Mailer\TestFiles\23784.png";
            using var stream = new MemoryStream(File.ReadAllBytes(filePath).ToArray());
            var formFile = new FormFile(stream, 0, stream.Length, "streamFile", filePath.Split(@"\").Last());
            var dummyAttachments = new List<IFormFile>();
            dummyAttachments.Add(formFile);
            var dummyForm = new FormEntity
            {
                FormId = 1, Customer = dummyCustomer, ServiceType = ServiceType.Calligraphy, StartingRate = 20.00f,
                Comments = "some text", Attachments = dummyAttachments
            };
            var dummyRequest = new MailRequest();

            _mockFormService.Setup(x => x.Create(dummyForm)).Returns(true);
            _mockMailerService.Setup(s => s.SendMailAsync(dummyRequest)).Returns(async () => { await Task.Yield(); });

            // Act
            Func<Task> result = () => _formController.Post(dummyForm);

            // Assert
            Assert.Empty(dummyForm.Customer.Email);
            var error = await Assert.ThrowsAsync<ArgumentException>(result);
            Assert.Equal("Email not found", error.Message);
        }

        //TC4-TC7
        [Fact]
        // Test to see if we get an exception when encountering an invalid email
        public async void PostBadRequestTestInvalidEmail()
        {
            // Arrange
            var dummyAddress = new AddressEntity
            {
                AddressId = 1, Street = "somne street", City = "some city", Country = "some country",
                Postal = "some code"
            };
            var dummyCustomer = new CustomerEntity
            {
                CustomerId = 1, FirstName = "some name", LastName = "some name", Address = dummyAddress,
                Email = "some email"
            };
            const string filePath = @"..\..\..\Mailer\TestFiles\23784.png";
            await using var stream = new MemoryStream((await File.ReadAllBytesAsync(filePath)).ToArray());
            var formFile = new FormFile(stream, 0, stream.Length, "streamFile", filePath.Split(@"\").Last());
            var dummyAttachments = new List<IFormFile> {formFile};
            var dummyForm = new FormEntity
            {
                FormId = 1, Customer = dummyCustomer, ServiceType = ServiceType.Calligraphy, StartingRate = 20.00f,
                Comments = "some text", Attachments = dummyAttachments
            };
            var dummyRequest = new MailRequest();

            _mockFormService.Setup(x => x.Create(dummyForm)).Returns(true);
            _mockMailerService.Setup(s => s.SendMailAsync(dummyRequest)).Returns(async () => { await Task.Yield(); });

            // Act
            Task Result()
            {
                return _formController.Post(dummyForm);
            }

            // Assert
            var error = await Assert.ThrowsAsync<FormatException>(Result);
            Assert.Equal("Not a valid email", error.Message);
        }

        //TC4-TC8
        [Fact]
        // Test GetAll
        // This will be done towards the end of the project
        public void GetAllFormsTest()
        {
            // Arrange
            var dummyForms = new List<FormEntity>();
            for (var i = 0; i < 10; i++)
            {
                var dummyAddress = new AddressEntity
                {
                    AddressId = 1, Street = "somne street", City = "some city", Country = "some country",
                    Postal = "some code"
                };
                var dummyCustomer = new CustomerEntity
                {
                    CustomerId = 1, FirstName = "some name", LastName = "some name", Address = dummyAddress,
                    Email = "some email"
                };
                var dummyAttachments = new List<IFormFile>();
                var dummyForm = new FormEntity
                {
                    FormId = 1, Customer = dummyCustomer, ServiceType = ServiceType.Calligraphy, StartingRate = 20.00f,
                    Comments = "some text", Attachments = dummyAttachments
                };
                dummyForms.Add(dummyForm);
            }

            var formParams = new FormParameters
            {
                PageNumber = 1,
                PageSize = 2
            };

            var paged = new PagedList<FormEntity>(dummyForms, 10, formParams.PageNumber, formParams.PageSize);

            var metadata = new
            {
                paged.TotalCount,
                paged.PageSize,
                PageNumber = paged.CurrentPage,
                paged.TotalPages,
                HasNext = true,
                HasPrevious = false
            };

            // mock http header X-Pagination with serialized PagedList
            var mockHttpContext = new HttpContextMock();
            mockHttpContext.Request.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            _formController.ControllerContext = new ControllerContext
            {
                HttpContext = mockHttpContext
            };

            _mockFormService.Setup(x => x.GetAll(formParams)).Returns(paged);

            // Act
            var result = _formController.GetAllPages(formParams);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
    }
}