using System;
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
        private readonly Mock<IImageService> _imageServiceMock;
        private readonly ImageController _imageController;
        
        public ImageControllerTests()
        {
            _imageServiceMock = new Mock<IImageService>();
            _imageController = new ImageController(_imageServiceMock.Object);
        }
        
        [Fact]
        //TC11
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
        //TC12
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
        
        [Fact]
        public void GetAll_ShouldReturnListOfTwoEntities()
        {
            // Arrange
            var images = new List<ImageEntity>()
            {
                new ImageEntity(){ Id = 1, ImageData = It.IsAny<string>() , ImageTitle = "Test1" },
                new ImageEntity(){ Id = 2, ImageData = It.IsAny<string>(), ImageTitle = "Test2" },
            };
            
            _imageServiceMock.Setup(x => x.GetAll()).Returns(images);
            
            // Act
            var result  = _imageController.GetAll();
            
            // Assert
            Assert.IsType<List<ImageEntity>>(result);
            Assert.Equal(2, result.Count());
        }
        
        [Fact]
        // TC7: Test for image upload
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
        
        [Fact]
        // TC8: Test for image upload
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
        
        [Fact]
        public void Update_ShouldReturnOkActionResult()
        {
            // Arrange
            var image = new ImageEntity();
            _imageServiceMock.Setup(x => x.Update(image, It.IsAny<int>() )).Returns(new OkObjectResult(image));
            
            // Act
            var result = _imageController.Update(image, It.IsAny<int>());
            
            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
        
        [Fact]
        public void Update_ShouldReturnBadRequestActionResult()
        {
            // Arrange
            var image = new ImageEntity();
            _imageServiceMock.Setup(x => x.Update(image, It.IsAny<int>() )).Returns(new BadRequestResult());
            
            // Act
            var result = _imageController.Update(image, It.IsAny<int>());
            
            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void Update_ShouldReturnNotFoundActionResult()
        {
            // Arrange
            var image = new ImageEntity();
            _imageServiceMock.Setup(x => x.Update(image, It.IsAny<int>() )).Returns(new NotFoundResult());
            
            // Act
            var result = _imageController.Update(image, It.IsAny<int>());
            
            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
        
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
