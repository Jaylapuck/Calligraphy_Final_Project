using System.Linq;
using Calligraphy.Data.Config;
using Calligraphy.Data.Models;
using Calligraphy.Data.Pagination;
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
        
        public PagedList<FormEntity> GetAll(FormParameters formParameters)
        {
            
            return PagedList<FormEntity>.ToPagedList(_context.Forms.OrderBy(x => x.CreatedDate), formParameters.PageNumber, formParameters.PageSize); 
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
