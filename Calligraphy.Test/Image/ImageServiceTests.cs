using Calligraphy.Business.Image;
using Calligraphy.Data.Models;
using Calligraphy.Data.Repo.Image;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Calligraphy.Test.Image
{
    public class ImageServiceTests
    {
        private readonly ImageService _formService;

        private readonly Mock<IImageRepo> _mockFormRepo;

        public ImageServiceTests()
        {
            _mockFormRepo = new Mock<IImageRepo>();
            _formService = new ImageService(_mockFormRepo.Object);
        }

        [Fact]
        //TC5-TS1
        public void GetImage_ShouldReturnOkActionResult()
        {
            // Arrange
            var image = new ImageEntity();
            _mockFormRepo.Setup(x => x.GetById(It.IsAny<int>())).Returns(image);

            // Act
            var result = _formService.GetById(1);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        //TC5-TS2
        public void GetImage_ShouldReturnNotFoundActionResult()
        {
            // Arrange
            _mockFormRepo.Setup(x => x.GetById(It.IsAny<int>())).Returns((ImageEntity) null);

            // Act
            var result = _formService.GetById(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        //TC5-TS3
        public void PostImage_ShouldReturnOkActionResult()
        {
            // Arrange
            var image = new ImageEntity();
            _mockFormRepo.Setup(x => x.Add(It.IsAny<ImageEntity>())).Returns(image);

            // Act
            var result = _formService.Create(image);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        //TC5-TS4
        public void PostImage_ShouldReturnBadRequestActionResult()
        {
            // Arrange
            _mockFormRepo.Setup(x => x.Add(null)).Returns((ImageEntity) null);

            // Act
            var result = _formService.Create(null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        //TC5-TS5
        public void PostImage_ShouldReturnBadRequestActionResult_WhenImageIdAlreadyExist()
        {
            // Arrange
            var image = new ImageEntity();
            _mockFormRepo.Setup(x => x.Add(It.IsAny<ImageEntity>())).Returns((ImageEntity) null);
            _mockFormRepo.Setup(x => x.GetByImageId(It.IsAny<int>())).Returns(image);

            // Act
            var result = _formService.Create(image);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        //TC5-TS6
        public void PutImage_ShouldReturnNotFoundResult()
        {
            // Arrange
            var image = new ImageEntity();
            _mockFormRepo.Setup(x => x.Update(It.IsAny<ImageEntity>())).Returns((ImageEntity) null);

            // Act
            var result = _formService.Update(image, It.IsAny<int>());

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        //TC5-TS7
        public void PutImage_ShouldReturnOkResult()
        {
            // Arrange
            var image = new ImageEntity();
            _mockFormRepo.Setup(x => x.GetByImageId(It.IsAny<int>())).Returns(image);
            _mockFormRepo.Setup(x => x.Update(It.IsAny<ImageEntity>())).Returns(image);

            // Act
            var result = _formService.Update(image, It.IsAny<int>());

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        //TC5-TS8
        [Fact]
        public void PutImage_ShouldReturnBadRequestResult()
        {
            // Act
            var result = _formService.Update(null, It.IsAny<int>());

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        //TC5-TS9
        [Fact]
        public void DeleteImage_ShouldReturnNotFoundResult()
        {
            // Arrange
            _mockFormRepo.Setup(x => x.DeleteById(It.IsAny<int>()));

            // Act
            var result = _formService.Delete(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        //TC5-TS10
        [Fact]
        public void DeleteImage_ShouldReturnOkResult()
        {
            // Arrange
            var image = new ImageEntity
            {
                Id = 1,
                ImageTitle = "Test",
                ImageData = It.IsAny<string>()
            };

            _mockFormRepo.Setup(x => x.GetByImageId(It.IsAny<int>())).Returns(image);
            _mockFormRepo.Setup(x => x.DeleteById(It.IsAny<int>()));

            // Act
            var result = _formService.Delete(1);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
    }
}