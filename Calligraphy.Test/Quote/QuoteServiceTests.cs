using Calligraphy.Business.Quote;
using Calligraphy.Data.Enums;
using Calligraphy.Data.Models;
using Calligraphy.Data.Repo.Quote;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        // TS2-TC5
        public void GetAllQuotes()
        {
            // Arrange
            FormEntity form = new FormEntity();
            var quotes = new List<QuoteEntity>
            {
                new QuoteEntity(){ QuoteId = 1, ApprovalStatus = Status.Approved, Materials = "Random Materials", Price = 25},
                new QuoteEntity(){ QuoteId = 2, ApprovalStatus = Status.Denied, Materials = "Random Materials", Price = 35 },
            };

            // Act
            _mockQuoteRepo.Setup(x => x.GetAll()).Returns(quotes);
            var result = _quoteService.GetAll();

            // Assert
            Assert.Equal(2, result.Count());
            foreach (QuoteEntity temp in result)
            {
                Assert.NotNull(temp.QuoteId);
            }
        }

        [Fact]
        // TS1-TC6
        public void CreateQuote()
        {
            FormEntity form = new FormEntity();
            // Arrange
            var quote = new QuoteEntity { QuoteId = 1, ApprovalStatus = Status.Approved, Materials = "Random Materials", Price = 25 };

            // Act
            _mockQuoteRepo.Setup(x => x.Create(quote));
            _quoteService.Create(quote);

            // Assert
            _mockQuoteRepo.Verify(x => x.Create(quote), Times.Once);
        }
    }
}
