using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Calligraphy.Business.Form;
using Calligraphy.Controllers;
using Calligraphy.Data.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Calligraphy.Tests.FormTests
{
    [TestClass]
    public class FormControllerTest
    {
        private Mock<IFormService> _mockFormService;
        private FormController _formController;

        [TestInitialize]
        public void Initialize()
        {
            _mockFormService = new Mock<IFormService>();
            _formController = new FormController(_mockFormService.Object);
        }
        
        [TestMethod]
        // test get all api
        public void GetAll()
        {
            // Arrange
            var expected = new List<FormEntity>();
            _mockFormService.Setup(x => x.GetAll()).Returns(expected);

            // Act
            var actual = _formController.Get();

            // Assert
            Assert.AreEqual(expected, actual);
        }
        
        [TestMethod]
        // test get all api, returns empty list
        public void GetAll_ReturnsNull()
        {
            // Arrange
            var expected = new List<Data.Models.FormEntity>();
            _mockFormService.Setup(x => x.GetAll()).Returns(expected);

            // Act
            var actual = _formController.Get();

            // Assert
            Assert.AreEqual(expected, actual);
        }
        
        [TestMethod]
        // test post api
        public void Post_ReturnsOkResult()
        {
            // Arrange
            var expected = new FormEntity();
            _mockFormService.Setup(x => x.Create(expected)).Returns(true);

            // Act
            var actual = _formController.Post(expected);

            // Assert
            Assert.IsInstanceOfType(actual, typeof(OkNegotiatedContentResult<FormEntity>));
        }
        
        [TestMethod]
        // test post api, returns bad request
        public void Post_ReturnsBadRequest()
        {
            // Arrange
            var expected = new FormEntity();
            _mockFormService.Setup(x => x.Create(expected)).Returns(false);

            // Act
            var actual = _formController.Post(null);

            // Assert
            Assert.IsInstanceOfType(actual, typeof(BadRequestResult));
        }
    }
}
