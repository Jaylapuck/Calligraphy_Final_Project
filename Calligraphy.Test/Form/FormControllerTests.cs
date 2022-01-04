﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calligraphy.Business.Form;
using Calligraphy.Controllers;
using Calligraphy.Data.Enums;
using Calligraphy.Data.Models;
using Calligraphy.Mailer.Services;
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
        // TC2-TC2
        public void Post_ReturnsOkResult()
        {
            // Arrange
            var expected = new FormEntity();
            _mockFormService.Setup(x => x.Create(expected)).Returns(true);

            // Act
            var actual = _formController.Post(expected);

            // Assert
            Assert.IsType<OkObjectResult>(actual);
        }

        [Fact]
        // TC2-TC4
        public void Post_ReturnsBadRequest()
        {
            // Arrange
            var expected = new FormEntity();
            _mockFormService.Setup(x => x.Create(expected)).Returns(false);

            // Act
            var actual = _formController.Post(null);

            // Assert
            Assert.IsType<BadRequestResult>(actual);
        }

        [Fact]
        public void GetAllServicesOK()
        {
            // Arrange
            List<ServiceEntity> dummyServices = new List<ServiceEntity>
            {
                new ServiceEntity{ServiceId = 1, TypeName = ServiceType.Calligraphy, StartingRate = 20.00f},
                new ServiceEntity{ServiceId = 2, TypeName = ServiceType.Engraving, StartingRate = 30.00f}
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
    }
}
