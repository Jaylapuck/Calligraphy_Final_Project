using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Calligraphy.Business.Form;
using Calligraphy.Controllers;
using Calligraphy.Data.Enums;
using Calligraphy.Data.Filters;
using Calligraphy.Data.Helpers;
using Calligraphy.Data.IUriService;
using Calligraphy.Data.Models;
using Calligraphy.Data.Repo.Wrappers;
using Calligraphy.Mailer.Model;
using Calligraphy.Mailer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Calligraphy.Test.Form
{
    public class FormControllerTests
    {
        private readonly Mock<IFormService> _mockFormService;
        private readonly Mock<IMailerService> _mockMailerService;
        private readonly Mock<PaginationHelper> _mockPaginationHelper;
        private readonly Mock<IUriService> _mockUriService;
        private readonly Mock<PaginationFilter> _mockPaginationFilter;
        private readonly FormController _formController;

        public FormControllerTests()
        {

            _mockFormService = new Mock<IFormService>();
            _mockPaginationHelper = new Mock<PaginationHelper>();
            _mockUriService = new Mock<IUriService>();
            _mockPaginationFilter = new Mock<PaginationFilter>();
            _mockMailerService = new Mock<IMailerService>();
            _formController = new FormController(_mockFormService.Object, _mockMailerService.Object);
        }

        [Fact]
        public void GetAllServicesOk()
        {
            // Arrange
            List<ServiceEntity> dummyServices = new List<ServiceEntity>
            {
                new ServiceEntity {ServiceId = 1, TypeName = ServiceType.Calligraphy, StartingRate = 20.00f},
                new ServiceEntity {ServiceId = 2, TypeName = ServiceType.Engraving, StartingRate = 30.00f}
            };

            _mockFormService.Setup(x => x.GetAllServices()).Returns(dummyServices);

            // Act
            var result = _formController.GetServices();

            // Assert
            Assert.IsType<List<ServiceEntity>>(result);
            Assert.Equal(2, dummyServices.Count);
        }

        [Fact]
        public void GetAllServicesEmpty()
        {
            // Arrange
            List<ServiceEntity> dummyServices = new List<ServiceEntity>();

            _mockFormService.Setup(x => x.GetAllServices()).Returns(dummyServices);

            // Act
            var result = _formController.GetServices();

            // Assert
            Assert.IsType<List<ServiceEntity>>(result);
            Assert.Empty(dummyServices);
        }

        [Fact]
        // Test to see if we get a successful post
        public async void PostOkResultTest()
        {
            // Arrange
            AddressEntity dummyAddress = new AddressEntity
            {
                AddressId = 1, Street = "somne street", City = "some city", Country = "some country",
                Postal = "some code"
            };
            CustomerEntity dummyCustomer = new CustomerEntity
            {
                CustomerId = 1, FirstName = "some name", LastName = "some name", Address = dummyAddress,
                Email = "tristanblacklafleur@hotmail.ca"
            };
            string filePath = @"..\..\..\Mailer\TestFiles\23784.png";
            using var stream = new MemoryStream((await File.ReadAllBytesAsync(filePath)).ToArray());
            var formFile = new FormFile(stream, 0, stream.Length, "streamFile", filePath.Split(@"\").Last());
            List<IFormFile> dummyAttachments = new List<IFormFile> {formFile};
            FormEntity dummyForm = new FormEntity
            {
                FormId = 1, Customer = dummyCustomer, ServiceType = ServiceType.Calligraphy, StartingRate = 20.00f,
                Comments = "some text", Attachments = dummyAttachments
            };
            MailRequest dummyRequest = new MailRequest();

            _mockFormService.Setup(x => x.Create(dummyForm)).Returns(true);
            _mockMailerService.Setup(s => s.SendMailAsync(dummyRequest)).Returns(async () => { await Task.Yield(); });

            // Act
            var actual = await _formController.Post(dummyForm);

            // Assert
            Assert.IsType<OkObjectResult>(actual);
        }

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

        [Fact]
        // Test to see if we get a successful post wo/an attachment
        public async void PostOkResultTestNoAttachments()
        {
            // Arrange
            AddressEntity dummyAddress = new AddressEntity
            {
                AddressId = 1, Street = "somne street", City = "some city", Country = "some country",
                Postal = "some code"
            };
            CustomerEntity dummyCustomer = new CustomerEntity
            {
                CustomerId = 1, FirstName = "some name", LastName = "some name", Address = dummyAddress,
                Email = "tristanblacklafleur@hotmail.ca"
            };
            FormEntity dummyForm = new FormEntity
            {
                FormId = 1, Customer = dummyCustomer, ServiceType = ServiceType.Calligraphy, StartingRate = 20.00f,
                Comments = "some text", Attachments = new List<IFormFile>()
            };
            MailRequest dummyRequest = new MailRequest();

            _mockFormService.Setup(x => x.Create(dummyForm)).Returns(true);
            _mockMailerService.Setup(s => s.SendMailAsync(dummyRequest)).Returns(async () => { await Task.Yield(); });

            // Act
            var actual = await _formController.Post(dummyForm);

            // Assert
            Assert.Empty(dummyForm.Attachments);
            Assert.IsType<OkObjectResult>(actual);
        }

        [Fact]
        // Test to see if we get an exception when encountering an empty email
        public async void PostBadRequestTestNoEmail()
        {
            // Arrange
            AddressEntity dummyAddress = new AddressEntity
            {
                AddressId = 1, Street = "somne street", City = "some city", Country = "some country",
                Postal = "some code"
            };
            CustomerEntity dummyCustomer = new CustomerEntity
                {CustomerId = 1, FirstName = "some name", LastName = "some name", Address = dummyAddress, Email = ""};
            string filePath = @"..\..\..\Mailer\TestFiles\23784.png";
            using var stream = new MemoryStream(File.ReadAllBytes(filePath).ToArray());
            var formFile = new FormFile(stream, 0, stream.Length, "streamFile", filePath.Split(@"\").Last());
            List<IFormFile> dummyAttachments = new List<IFormFile>();
            dummyAttachments.Add(formFile);
            FormEntity dummyForm = new FormEntity
            {
                FormId = 1, Customer = dummyCustomer, ServiceType = ServiceType.Calligraphy, StartingRate = 20.00f,
                Comments = "some text", Attachments = dummyAttachments
            };
            MailRequest dummyRequest = new MailRequest();

            _mockFormService.Setup(x => x.Create(dummyForm)).Returns(true);
            _mockMailerService.Setup(s => s.SendMailAsync(dummyRequest)).Returns(async () => { await Task.Yield(); });

            // Act
            Func<Task> result = () => _formController.Post(dummyForm);

            // Assert
            Assert.Empty(dummyForm.Customer.Email);
            var error = await Assert.ThrowsAsync<ArgumentException>(result);
            Assert.Equal("Email not found", error.Message);
        }

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
            Task Result() => _formController.Post(dummyForm);

            // Assert
            var error = await Assert.ThrowsAsync<FormatException>(Result);
            Assert.Equal("Not a valid email", error.Message);
        }

        [Fact]
        // Test GetAll
        // This will be done towards the end of the project
        public void GetAllTest()
        {
            /*
            // Arrange
            var dummyForms = new List<FormEntity>
            {
                new() {FormId = 1, ServiceType = ServiceType.Calligraphy, Comments = "Comments 1"},
                new() {FormId = 2, ServiceType = ServiceType.Calligraphy, Comments = "Comments 2"},
                new() {FormId = 3, ServiceType = ServiceType.Calligraphy, Comments = "Comments 3"}
            };
            
            // Pagination Filter
            var dummyFilter = new PaginationFilter
            {
                PageNumber = 1,
                PageSize = 2
            };
            
            var dummyUri = new Uri("http://localhost:5000/api/form");
            
            //mock Route
            var dummyRoute = 
                $"{dummyUri.AbsolutePath}?pageNumber={dummyFilter.PageNumber}&pageSize={dummyFilter.PageSize}";
            
            var pagedResponse = new PagedResponse<IEnumerable<FormEntity>>(dummyForms, dummyFilter.PageNumber,
                dummyFilter.PageSize);
            
            _mockFormService.Setup(x => x.GetAll(dummyFilter, dummyRoute )).Returns(new OkObjectResult(pagedResponse));
            
            // Act
            var result = _formController.GetAllPages(dummyFilter);
            
            Assert.IsType<OkObjectResult>(result);
            */
        }
    }
}
