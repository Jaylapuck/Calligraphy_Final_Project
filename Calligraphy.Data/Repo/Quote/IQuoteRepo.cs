using System.Collections.Generic;
using Calligraphy.Data.Models;

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