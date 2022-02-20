using System.Collections.Generic;
using System.Linq;
using Calligraphy.Business.Contract;
using Calligraphy.Business.Quote;
using Calligraphy.Controllers;
using Calligraphy.Data.Enums;
using Calligraphy.Data.Models;
using Calligraphy.Mailer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace Calligraphy.Test.Quote
{
    public class QuoteControllerTests
    {
        private readonly Mock<IContractService> _contractServiceMock;
        private readonly Mock<IMailerService> _mailServiceMock;
        private readonly QuoteController _quoteController;
        private readonly Mock<IQuoteService> _quoteServiceMock;
        private readonly Mock<IConfiguration> _mockConfig;

        public QuoteControllerTests()
        {
            _quoteServiceMock = new Mock<IQuoteService>();
            _mailServiceMock = new Mock<IMailerService>();
            _contractServiceMock = new Mock<IContractService>();
            _quoteController = new QuoteController(_quoteServiceMock.Object, _mailServiceMock.Object,
                _contractServiceMock.Object, _mockConfig.Object);
        }

        [Fact]
        //TC7-TC1
        public void GetByFormId_ShouldReturnOk()
        {
            // Arrange
            var form_id = 1;
            var quote = new QuoteEntity();
            _quoteServiceMock.Setup(x => x.GetByFormId(form_id)).Returns(new OkObjectResult(quote));

            // Act
            var result = _quoteController.GetByFormId(form_id);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        //TC7-TC2
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

        //TC7-TC3
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

        //TC7-TC4
        [Fact]
        public void GetAll_ShouldReturnListOfTwoEntities()
        {
            // Arrange
            var form = new FormEntity();
            var quotes = new List<QuoteEntity>
            {
                new() {QuoteId = 1, ApprovalStatus = Status.Approved, Materials = "Random Materials", Price = 25},
                new() {QuoteId = 2, ApprovalStatus = Status.Denied, Materials = "Random Materials", Price = 35}
            };

            _quoteServiceMock.Setup(x => x.GetAll()).Returns(quotes);

            // Act
            var result = _quoteController.GetAll();

            // Assert
            Assert.IsType<List<QuoteEntity>>(result);
            Assert.Equal(2, result.Count());
        }

        //TC7-TC5
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

        //TC7-TC6
        [Fact]
        // Test to see if we get a successful post
        public void CreateOKResultTest()
        {
            // Arrange
            var form = new FormEntity();
            var dummyQuote = new QuoteEntity
                {QuoteId = 1, ApprovalStatus = Status.Approved, Materials = "Random Materials", Price = 25};

            _quoteServiceMock.Setup(x => x.Create(dummyQuote)).Returns(true);

            // Act
            var actual = _quoteController.Create(dummyQuote);

            // Assert
            Assert.IsType<OkObjectResult>(actual);
        }

        //TC7-TC7
        [Fact]
        // Test to see if we get a failed post
        public void CreateBadResultTest()
        {
            // Arrange
            var form = new FormEntity();
            var dummyQuote = new QuoteEntity
                {QuoteId = 1, ApprovalStatus = Status.Approved, Materials = "Random Materials", Price = 25};

            _quoteServiceMock.Setup(x => x.Create(dummyQuote)).Returns(false);

            // Act
            var actual = _quoteController.Create(dummyQuote);

            // Assert
            Assert.IsType<BadRequestResult>(actual);
        }

        //TC7-TC8
        [Fact]
        public async void Update_ShouldReturnOkActionResult()
        {
            // Arrange
            var quote = new QuoteEntity();
            _quoteServiceMock.Setup(x => x.Update(quote, It.IsAny<int>())).Returns(new OkObjectResult(quote));

            // Act
            var result = await _quoteController.Update(quote, It.IsAny<int>());

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        //TC7-TC9
        [Fact]
        public async void Update_ShouldReturnBadRequestActionResult()
        {
            // Arrange
            var quote = new QuoteEntity();
            _quoteServiceMock.Setup(x => x.Update(quote, It.IsAny<int>())).Returns(new BadRequestResult());

            // Act
            var result = await _quoteController.Update(quote, It.IsAny<int>());

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        //TC7-TC10
        [Fact]
        public async void Update_ShouldReturnNotFoundActionResult()
        {
            // Arrange
            var quote = new QuoteEntity();
            _quoteServiceMock.Setup(x => x.Update(quote, It.IsAny<int>())).Returns(new NotFoundResult());

            // Act
            var result = await _quoteController.Update(quote, It.IsAny<int>());

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}