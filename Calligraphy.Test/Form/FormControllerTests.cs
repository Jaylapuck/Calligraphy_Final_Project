using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calligraphy.Business.Form;
using Calligraphy.Controllers;
using Calligraphy.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Calligraphy.Test.Form
{
    public class FormControllerTests
    {
        private readonly Mock<IFormService> _mockFormService;
        private readonly FormController _formController;

        public FormControllerTests()
        {
            _mockFormService = new Mock<IFormService>();
            _formController = new FormController(_mockFormService.Object);
        }

        // TS2-TC1
        [Fact]
        // test get all api, returns list of forms
        public void GetAll()
        {
            // Arrange
            var forms = new List<FormEntity>
            {
                new FormEntity {  FormId = 1, ServiceType = "Form 1", Comments = "Description 1"},
                new FormEntity { FormId = 2, ServiceType = "Form 2", Comments = "Description 2"}
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
    }
}
