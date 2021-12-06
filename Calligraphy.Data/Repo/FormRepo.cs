using Calligraphy.Data.Config;
using Calligraphy.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calligraphy.Data.Repo
{
    public class FormRepo : IFormRepo
    {
        private readonly CalligraphyContext _context;
        
        public FormRepo(CalligraphyContext context)
        {
            _context = context;
        }

        public FormRepo()
        {
            _context = new CalligraphyContext();
        }
        
        public IEnumerable<FormEntity> GetAll()
        {
            using (_context)
            {
                return _context.Forms.ToList();
            }
        }

        public FormEntity Create(FormEntity form)
        {
            using (_context)
            {
                _context.Forms.Add(form);
                _context.SaveChanges();
                return form;
            }
        }
    }
}
