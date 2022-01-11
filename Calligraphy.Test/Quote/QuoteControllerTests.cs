using Calligraphy.Business.Quote;
using Calligraphy.Controllers;
using Calligraphy.Data.Enums;
using Calligraphy.Data.Models;
using Calligraphy.Mailer.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Calligraphy.Test.Quote
{
    public class QuoteControllerTests
    {
        private readonly Mock<IQuoteService> _quoteServiceMock;
        private readonly QuoteController _quoteController;
        public QuoteControllerTests()
        {
            _quoteServiceMock = new Mock<IQuoteService>();
            _quoteController = new QuoteController(_quoteServiceMock.Object);
        }

        [Fact]
        //TC11
        public void GetByFormId_ShouldReturnOk()
        {
            // Arrange
            int form_id = 1;
            var quote = new QuoteEntity();
            _quoteServiceMock.Setup(x => x.GetByFormId(form_id)).Returns(new OkObjectResult(quote));

            // Act
            var result = _quoteController.GetByFormId(form_id);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        //TC12
        public void GetById_ShouldReturnNotFound()
        {
            // Arrange
            const int form_id = 1;
            _quoteServiceMock.Setup(x => x.GetByFormId(form_id)).Returns(new NotFoundResult());

            // Act
            var result = _quoteController.GetByFormId(form_id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetAll_ShouldReturnEmptyList()
        {
            // Arrange
            var quotes = new List<QuoteEntity>();
            _quoteServiceMock.Setup(x => x.GetAll()).Returns(quotes);

            // Act
            var result = _quoteController.GetAll();

            // Assert
            Assert.Empty(quotes);
        }

        [Fact]
        public void GetAll_ShouldReturnListOfTwoEntities()
        {
            // Arrange
            FormEntity form = new FormEntity();
            var quotes = new List<QuoteEntity>()
            {
                new QuoteEntity(){ QuoteId = 1, ApprovalStatus = Status.Approved, Materials = "Random Materials", Price = 25},
                new QuoteEntity(){ QuoteId = 2, ApprovalStatus = Status.Denied, Materials = "Random Materials", Price = 35 },
            };

            _quoteServiceMock.Setup(x => x.GetAll()).Returns(quotes);

            // Act
            var result = _quoteController.GetAll();

            // Assert
            Assert.IsType<List<QuoteEntity>>(result);
            Assert.Equal(2, result.Count());
        }

        // TS2-TC3
        [Fact]
        // test get all api, returns empty list
        public void GetAll_ReturnsEmptyList()
        {
            // Arrange
            var quotes = new List<QuoteEntity>();

            _quoteServiceMock.Setup(x => x.GetAll()).Returns(quotes);

            // Act
            var result = _quoteController.GetAll();

            // Assert
            Assert.IsType<List<QuoteEntity>>(result);
            Assert.Empty(result);
        }

        [Fact]
        // Test to see if we get a successful post
        public void CreateOKResultTest()
        {
            // Arrange
            FormEntity form = new FormEntity();
            QuoteEntity dummyQuote = new QuoteEntity() { QuoteId = 1, ApprovalStatus = Status.Approved, Materials = "Random Materials", Price = 25 };

            _quoteServiceMock.Setup(x => x.Create(dummyQuote)).Returns(true);

            // Act
            var actual = _quoteController.Create(dummyQuote);

            // Assert
            Assert.IsType<OkObjectResult>(actual);
        }

        [Fact]
        // Test to see if we get a failed post
        public void CreateBadResultTest()
        {
            // Arrange
            FormEntity form = new FormEntity();
            QuoteEntity dummyQuote = new QuoteEntity() { QuoteId = 1, ApprovalStatus = Status.Approved, Materials = "Random Materials", Price = 25 };

            _quoteServiceMock.Setup(x => x.Create(dummyQuote)).Returns(false);

            // Act
            var actual = _quoteController.Create(dummyQuote);

            // Assert
            Assert.IsType<BadRequestResult>(actual);
        }

        [Fact]
        public void Update_ShouldReturnOkActionResult()
        {
            // Arrange
            var quote = new QuoteEntity();
            _quoteServiceMock.Setup(x => x.Update(quote, It.IsAny<int>())).Returns(new OkObjectResult(quote));

            // Act
            var result = _quoteController.Update(quote, It.IsAny<int>());

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Update_ShouldReturnBadRequestActionResult()
        {
            // Arrange
            var quote = new QuoteEntity();
            _quoteServiceMock.Setup(x => x.Update(quote, It.IsAny<int>())).Returns(new BadRequestResult());

            // Act
            var result = _quoteController.Update(quote, It.IsAny<int>());

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void Update_ShouldReturnNotFoundActionResult()
        {
            // Arrange
            var quote = new QuoteEntity();
            _quoteServiceMock.Setup(x => x.Update(quote, It.IsAny<int>())).Returns(new NotFoundResult());

            // Act
            var result = _quoteController.Update(quote, It.IsAny<int>());

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}

