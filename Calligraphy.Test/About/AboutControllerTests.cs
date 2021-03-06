using Calligraphy.Business.About;
using Calligraphy.Controllers;
using Calligraphy.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Calligraphy.Test.About
{
    public class AboutControllerTests
    {
        private readonly AboutController _aboutController;
        private readonly Mock<IAboutService> _mockAboutService;

        public string name1 = "name1",
            email1 = "email1",
            phone1 = "phone1",
            proffesion1 = "prof1",
            description1 = "desc1",
            language1 = "lang1",
            country1 = "count1",
            experience1 = "exp1",
            mission1 = "miss1";

        public AboutControllerTests()
        {
            _mockAboutService = new Mock<IAboutService>();
            _aboutController = new AboutController(_mockAboutService.Object);
        }

        // TC8-TC1
        [Fact]
        // test get  api, returns the baout information
        public void Get()
        {
            // Arrange
            var about = new AboutEntity
            {
                AboutId = 1,
                Name = name1,
                Email = email1,
                Phone = phone1,
                Profession = proffesion1,
                Description = description1,
                Language = language1,
                Country = country1,
                Experience = experience1,
                Mission = mission1
            };

            _mockAboutService.Setup(x => x.Get()).Returns(new OkObjectResult(about));

            // Act
            var result = _aboutController.Get();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        //TC8-TC1
        public void Get_ShouldReturnNotFound()
        {
            // Arrange
            _mockAboutService.Setup(x => x.Get()).Returns(new NotFoundResult());

            // Act
            var result = _aboutController.Get();

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        //TC8-TC2
        [Fact]
        public void Update_ShouldReturnBadRequestActionResult()
        {
            // Arrange
            var about = new AboutEntity();
            _mockAboutService.Setup(x => x.Update(about)).Returns(new BadRequestResult());

            // Act
            var result = _aboutController.Update(about);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        //TC8-TC10
        [Fact]
        public void Update_ShouldReturnNotFoundActionResult()
        {
            // Arrange
            var about = new AboutEntity();
            _mockAboutService.Setup(x => x.Update(about)).Returns(new NotFoundResult());

            // Act
            var result = _aboutController.Update(about);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}