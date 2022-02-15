using System.Collections.Generic;
using Calligraphy.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace Calligraphy.Business.Quote
{
    public interface IQuoteService
    {
        // GET ALL
        IEnumerable<QuoteEntity> GetAll();

        //GET By FormID
        IActionResult GetByFormId(int id);

        // CREATE
        bool Create(QuoteEntity quote);

        //UPDATE
        IActionResult Update(QuoteEntity quote, int id);
    }
}