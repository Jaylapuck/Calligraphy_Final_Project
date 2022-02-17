using System.Collections.Generic;
using System.Linq;
using Calligraphy.Business.Image;
using Calligraphy.Controllers;
using Calligraphy.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Calligraphy.Test.Image
{
    public class ImageControllerTests
    {
        private readonly ImageController _imageController;
        private readonly Mock<IImageService> _imageServiceMock;

        public ImageControllerTests()
        {
            _imageServiceMock = new Mock<IImageService>();
            _imageController = new ImageController(_imageServiceMock.Object);
        }

        [Fact]
        //TC5-TC1
        public void GetById_ShouldReturnOk()
        {
            // Arrange
            const int imageId = 1;
            var image = new ImageEntity();
            _imageServiceMock.Setup(x => x.GetById(imageId)).Returns(new OkObjectResult(image));

            // Act
            var result = _imageController.GetById(imageId);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        //TC5-TC2
        public void GetById_ShouldReturnNotFound()
        {
            // Arrange
            const int imageId = 1;
            _imageServiceMock.Setup(x => x.GetById(imageId)).Returns(new NotFoundResult());

            // Act
            var result = _imageController.GetById(imageId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        //TC5-TC3
        [Fact]
        public void GetAll_ShouldReturnEmptyList()
        {
            // Arrange
            var images = new List<ImageEntity>();
            _imageServiceMock.Setup(x => x.GetAll()).Returns(images);

            // Act
            var result = _imageController.GetAll();

            // Assert
            Assert.Empty(images);
        }

        //TC5-TC4
        [Fact]
        public void GetAll_ShouldReturnListOfTwoEntities()
        {
            // Arrange
            var images = new List<ImageEntity>
            {
                new() {Id = 1, ImageData = It.IsAny<string>(), Title = "Test1"},
                new() {Id = 2, ImageData = It.IsAny<string>(), Title = "Test2"}
            };

            _imageServiceMock.Setup(x => x.GetAll()).Returns(images);

            // Act
            var result = _imageController.GetAll();

            // Assert
            Assert.IsType<List<ImageEntity>>(result);
            Assert.Equal(2, result.Count());
        }

        //TC5-TC5
        [Fact]
        public void Create_ShouldReturnOkActionResult()
        {
            // Arrange
            var image = new ImageEntity();
            _imageServiceMock.Setup(x => x.Create(image)).Returns(new OkObjectResult(image));

            // Act
            var result = _imageController.Create(image);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        //TC5-TC6
        [Fact]
        public void Create_ShouldReturnBadRequestActionResult()
        {
            // Arrange
            var image = new ImageEntity();
            _imageServiceMock.Setup(x => x.Create(image)).Returns(new BadRequestResult());

            // Act
            var result = _imageController.Create(image);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        //TC5-TC7
        [Fact]
        public void Update_ShouldReturnOkActionResult()
        {
            // Arrange
            var image = new ImageEntity();
            _imageServiceMock.Setup(x => x.Update(image, It.IsAny<int>())).Returns(new OkObjectResult(image));

            // Act
            var result = _imageController.Update(image, It.IsAny<int>());

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        //TC5-TC8
        [Fact]
        public void Update_ShouldReturnBadRequestActionResult()
        {
            // Arrange
            var image = new ImageEntity();
            _imageServiceMock.Setup(x => x.Update(image, It.IsAny<int>())).Returns(new BadRequestResult());

            // Act
            var result = _imageController.Update(image, It.IsAny<int>());

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        //TC5-TC9
        [Fact]
        public void Update_ShouldReturnNotFoundActionResult()
        {
            // Arrange
            var image = new ImageEntity();
            _imageServiceMock.Setup(x => x.Update(image, It.IsAny<int>())).Returns(new NotFoundResult());

            // Act
            var result = _imageController.Update(image, It.IsAny<int>());

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        //TC5-TC10
        [Fact]
        public void Delete_ShouldReturnOkActionResult()
        {
            // Arrange
            const int imageId = 1;
            _imageServiceMock.Setup(x => x.Delete(imageId)).Returns(new OkResult());

            // Act
            var result = _imageController.Delete(imageId);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        //TC5-TC11
        [Fact]
        public void Delete_ShouldReturnNotFoundActionResult()
        {
            // Arrange
            var imageId = 1;
            _imageServiceMock.Setup(x => x.Delete(imageId)).Returns(new NotFoundResult());

            // Act
            var result = _imageController.Delete(imageId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}