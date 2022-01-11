using Calligraphy.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Calligraphy.Data.Repo.Quote
{
    public interface IQuoteRepo
    {
        IEnumerable<QuoteEntity> GetAll();

        QuoteEntity GetByFormId(int formId);

        bool Create(QuoteEntity quote);

        QuoteEntity Update(QuoteEntity quote);


    }
}
