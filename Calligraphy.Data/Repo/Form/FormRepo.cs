using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Calligraphy.Data.Config;
using Calligraphy.Data.Filters;
using Calligraphy.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Calligraphy.Data.Repo.Form
{
    public class FormRepo : IFormRepo
    {
        private readonly CalligraphyContext _context;
        private readonly DbContextOptions<CalligraphyContext> options;


        public FormRepo(CalligraphyContext context)
        {
            _context = context;
        }

        public FormRepo()
        {
            _context = new CalligraphyContext(options);
        }
        
        public IEnumerable<FormEntity> GetAll(PaginationFilter validFilter, out int totalRecords)
        {
            var list = _context.Forms.Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .Include(nameof(FormEntity.Customer))
                .Include(nameof(FormEntity.Quote))
                .AsNoTracking().ToList();
            
            totalRecords = _context.Forms.Count();
            
            return list;
        }
        
        public bool Create(FormEntity form)
        {
            using (_context)
            {
                _context.Forms.Add(form);
                _context.SaveChanges();
                return true;
            }
        }
    }
}
