using Calligraphy.Data.Config;
using Calligraphy.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Calligraphy.Data.Repo.Quote
{
    public class QuoteRepo : IQuoteRepo
    {
        private readonly CalligraphyContext _context;
        private readonly DbContextOptions<CalligraphyContext> options;


        public QuoteRepo(CalligraphyContext context)
        {
            _context = context;
        }

        public QuoteRepo()
        {
            _context = new CalligraphyContext(options);
        }

        public IEnumerable<QuoteEntity> GetAll()
        {
            using (_context)
            {
                return _context.Quotes.ToList();
            }
        }

        public QuoteEntity GetByFormId(int formId)
        {
            return _context.Quotes.FirstOrDefault(x => x.Form.FormId == formId);
        }

        public bool Create(QuoteEntity quote)
        {
            using (_context)
            {
                _context.Quotes.Add(quote);
                _context.SaveChanges();
                return true;
            }
        }
        public QuoteEntity Update(QuoteEntity quote)
        {
            _context.Entry(quote).State = EntityState.Modified;
            _context.SaveChanges();
            return quote;
        }

    }
}
