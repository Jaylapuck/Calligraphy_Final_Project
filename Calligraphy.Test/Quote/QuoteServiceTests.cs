using System.Collections.Generic;
using System.Linq;
using Calligraphy.Business.Quote;
using Calligraphy.Data.Enums;
using Calligraphy.Data.Models;
using Calligraphy.Data.Repo.Quote;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Calligraphy.Test.Quote
{
    public class QuoteServiceTests
    {
        private readonly Mock<IQuoteRepo> _mockQuoteRepo;
        private readonly QuoteService _quoteService;

        public QuoteServiceTests()
        {
            _mockQuoteRepo = new Mock<IQuoteRepo>();
            _quoteService = new QuoteService(_mockQuoteRepo.Object);
        }

        [Fact]
        // TS7-TS1
        public void GetAllQuotes()
        {
            // Arrange
            var form = new FormEntity();
            var quotes = new List<QuoteEntity>
            {
                new() {QuoteId = 1, ApprovalStatus = Status.Approved, Materials = "Random Materials", Price = 25},
                new() {QuoteId = 2, ApprovalStatus = Status.Denied, Materials = "Random Materials", Price = 35}
            };

            // Act
            _mockQuoteRepo.Setup(x => x.GetAll()).Returns(quotes);
            var result = _quoteService.GetAll();

            // Assert
            Assert.Equal(2, result.Count());
            foreach (var temp in result) Assert.NotNull(temp.QuoteId);
        }

        [Fact]
        // TS7-TS2
        public void CreateQuote()
        {
            var form = new FormEntity();
            // Arrange
            var quote = new QuoteEntity
                {QuoteId = 1, ApprovalStatus = Status.Approved, Materials = "Random Materials", Price = 25};

            // Act
            _mockQuoteRepo.Setup(x => x.Create(quote));
            _quoteService.Create(quote);

            // Assert
            _mockQuoteRepo.Verify(x => x.Create(quote), Times.Once);
        }

        [Fact]
        // TS7-TS2
        public void UpdateQuoteShouldReturnOkObjectResult()
        {
            // Arrange
            var OkId = 1;
            var quote = new QuoteEntity
                {ApprovalStatus = Status.Pending, Materials = "Random Materials", Price = 25, Duration = 14};

            _mockQuoteRepo.Setup(x => x.Update(It.IsAny<QuoteEntity>())).Returns(quote);
            _mockQuoteRepo.Setup(x => x.GetByFormId(It.IsAny<int>())).Returns(quote);

            // Act
            var actual = _quoteService.Update(quote, OkId);

            // Assert
            Assert.IsType<int>(OkId);
            Assert.IsType<OkObjectResult>(actual);
        }

        [Fact]
        // TS7-TS2
        public void UpdateQuoteShouldReturnBadRequestResult()
        {
            // Arrange
            var OkId = 1;
            QuoteEntity quote = null;

            // Act
            var actual = _quoteService.Update(quote, OkId);

            // Assert
            Assert.IsType<int>(OkId);
            Assert.IsType<BadRequestResult>(actual);
        }

        [Fact]
        // TS7-TS2
        public void UpdateQuoteShouldReturnNotFoundResult()
        {
            // Arrange
            var OkId = 1;
            var quote = new QuoteEntity
                {ApprovalStatus = Status.Pending, Materials = "Random Materials", Price = 25, Duration = 14};

            _mockQuoteRepo.Setup(x => x.Update(It.IsAny<QuoteEntity>())).Returns(quote);
            _mockQuoteRepo.Setup(x => x.GetByFormId(It.IsAny<int>())).Returns((QuoteEntity) null);

            // Act
            var actual = _quoteService.Update(quote, OkId);

            // Assert
            Assert.IsType<int>(OkId);
            Assert.IsType<NotFoundResult>(actual);
        }
    }
}