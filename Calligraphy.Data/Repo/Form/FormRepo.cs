using System.Collections.Generic;
using System.Linq;
using Calligraphy.Data.Config;
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
        
        public IEnumerable<FormEntity> GetAll()
        {
            using (_context)
            {
                return _context.Forms.ToList();
            }
        }

        public IEnumerable<FormEntity> GetAllPageable(int pageNumber, int pageSize)
        {
            using (_context)
            {
                return _context.Forms.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            }
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
