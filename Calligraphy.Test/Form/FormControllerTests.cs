using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calligraphy.Business.Form;
using Calligraphy.Controllers;
using Calligraphy.Data.Enums;
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
            
            var address1 = new AddressEntity { AddressId = 1, StreetAddress = "1000 Rue ThisSide", PostalCode = "J1Y1P1", City = "Montreal", Country = "Canada" };
            var address2 = new AddressEntity { AddressId = 2, StreetAddress = "2000 Rue OtherSide", PostalCode = "J2Y2P2", City = "Ottawa", Country = "Canada" };
            // Arrange
            var forms = new List<FormEntity>
            {
                new FormEntity {FormId = 1, FirstName="James", LastName="Albe", Address=address1, ServiceType = ServiceType.Calligraphy, Comments = "Comments 1"},
                new FormEntity {FormId = 2, FirstName="John", LastName="Doe", Address=address2, ServiceType = ServiceType.Engraving, Comments = "Comments 2"}
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
