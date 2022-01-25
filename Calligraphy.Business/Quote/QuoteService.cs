using Calligraphy.Data.Models;
using Calligraphy.Data.Repo.Quote;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Calligraphy.Business.Quote
{
    public class QuoteService : IQuoteService
    {
        private readonly IQuoteRepo _quoteRepo;

        public QuoteService(IQuoteRepo quoteRepo)
        {
            _quoteRepo = quoteRepo;
        }

        public IEnumerable<QuoteEntity> GetAll()
        {
            return _quoteRepo.GetAll();
        }
        public IActionResult GetByFormId(int form_id)
        {
            var quote = _quoteRepo.GetByFormId(form_id);
            if (quote == null)
            {
                return new NotFoundResult();
            }
            return new OkObjectResult(quote);
        }

        public bool Create(QuoteEntity quote)
        {
            return _quoteRepo.Create(quote);
        }


        public IActionResult Update(QuoteEntity quote, int form_id)
        {
            if (quote == null)
            {
                return new BadRequestResult();
            }
            var quoteToUpdate = _quoteRepo.GetByFormId(form_id);
            if (quoteToUpdate == null)
            {
                return new NotFoundResult();
            }
            quoteToUpdate.Price = quote.Price;
            quoteToUpdate.Duration = quote.Duration;
            quoteToUpdate.Materials = quote.Materials;
            quoteToUpdate.ApprovalStatus = quote.ApprovalStatus;
            _quoteRepo.Update(quoteToUpdate);
            return new OkObjectResult(quoteToUpdate);
        }
    }
}