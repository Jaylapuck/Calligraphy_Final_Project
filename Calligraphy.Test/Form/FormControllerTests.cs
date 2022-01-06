﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calligraphy.Business.Form;
using Calligraphy.Controllers;
using Calligraphy.Data.Enums;
using Calligraphy.Data.Models;
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
        private readonly FormController _formController;

        public FormControllerTests()
        {
            _mockFormService = new Mock<IFormService>();
            _mockMailerService = new Mock<IMailerService>();
            _formController = new FormController(_mockFormService.Object, _mockMailerService.Object);
        }

        // TS2-TC1
        [Fact]
        // test get all api, returns list of forms
        public void GetAll()
        {
            // Arrange
            var forms = new List<FormEntity>
            {
                new FormEntity {  FormId = 1, ServiceType = ServiceType.Calligraphy, Comments = "Description 1"},
                new FormEntity { FormId = 2, ServiceType = ServiceType.Engraving, Comments = "Description 2"}
            };

            _mockFormService.Setup(x => x.GetAll()).Returns(forms);

            // Act
            var result = _formController.Get();

            // Assert
            Assert.IsType<List<FormEntity>>(result);
            Assert.Equal(2, result.Count());
        }

        // TS2-TC3
        [Fact]
        // test get all api, returns empty list
        public void GetAll_ReturnsEmptyList()
        {
            // Arrange
            var forms = new List<FormEntity>();

            _mockFormService.Setup(x => x.GetAll()).Returns(forms);

            // Act
            var result = _formController.Get();

            // Assert
            Assert.IsType<List<FormEntity>>(result);
            Assert.Empty(result);
        }

        [Fact]
        // Test to see if we get a successful post
        public async void PostOKResultTest()
        {
            // Arrange
            AddressEntity dummyAddress = new AddressEntity{ AddressId = 1, Street = "somne street", City = "some city", Country = "some country", Postal = "some code" };
            CustomerEntity dummyCustomer = new CustomerEntity { CustomerId = 1, FirstName = "some name", LastName = "some name", Address = dummyAddress, Email = "tristanblacklafleur@hotmail.ca" };
            string filePath = @"..\..\..\Mailer\TestFiles\23784.png";
            using var stream = new MemoryStream(File.ReadAllBytes(filePath).ToArray());
            var formFile = new FormFile(stream, 0, stream.Length, "streamFile", filePath.Split(@"\").Last());
            List<IFormFile> dummyAttachments = new List<IFormFile>();
            FormEntity dummyForm = new FormEntity { FormId = 1, Customer = dummyCustomer, ServiceType = ServiceType.Calligraphy, StartingRate = 20.00, Comments = "some text", Attachments = dummyAttachments };
            MailRequest dummyRequest = new MailRequest();

            _mockFormService.Setup(x => x.Create(dummyForm)).Returns(true);
            _mockMailerService.Setup(s => s.SendMailAsync(dummyRequest)).Returns(async () => { await Task.Yield(); });

            // Act
            var actual = await _formController.Post(dummyForm);

            // Assert
            Assert.IsType<OkObjectResult>(actual);
        }
    }
}
