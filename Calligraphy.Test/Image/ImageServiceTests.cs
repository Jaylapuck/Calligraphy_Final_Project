using System;
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
        
        private readonly Mock<IImageRepo> _mockFormRepo;
        private readonly ImageService _formService;
        
        public ImageServiceTests()
        {
            _mockFormRepo = new Mock<IImageRepo>();
            _formService = new ImageService(_mockFormRepo.Object);
        }
        
        [Fact]
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
        public void PostImage_ShouldReturnBadRequestActionResult()
        {
            // Arrange
            var image = new ImageEntity();
            _mockFormRepo.Setup(x => x.Add(It.IsAny<ImageEntity>())).Returns((ImageEntity) null);
            
            // Act
            var result = _formService.Create(image);
            
            // Assert
            Assert.IsType<BadRequestResult>(result);
        }
        
        [Fact]
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
        public void PutImage_ShouldReturnOkResult()
        {
            // Arrange
            var image = new ImageEntity()
            {
                Id = 1,
                ImageTitle = "Test",
                ImageData = It.IsAny<byte[]>()
            };
            
            _mockFormRepo.Setup(x => x.GetById(1)).Returns(image);
            _mockFormRepo.Setup(x => x.Update(It.IsAny<ImageEntity>())).Returns(image);
            
            // Act
            var result = _formService.Update(image, 1);
            
            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void PutImage_ShouldReturnBadRequestResult()
        {
            // Act
            var result = _formService.Update(null, It.IsAny<int>());
            
            // Assert
            Assert.IsType<BadRequestResult>(result);
        }
        
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
        
        [Fact]
        public void DeleteImage_ShouldReturnOkResult()
        {
            // Arrange
            var image = new ImageEntity()
            {
                Id = 1,
                ImageTitle = "Test",
                ImageData = It.IsAny<byte[]>()
            };
            
            _mockFormRepo.Setup(x => x.GetById(1)).Returns(image);
            _mockFormRepo.Setup(x => x.DeleteById(It.IsAny<int>()));
            
            // Act
            var result = _formService.Delete(1);
            
            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

    }
}