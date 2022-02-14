using System.Linq;
using Calligraphy.Data.Config;
using Calligraphy.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Calligraphy.Data.Repo.About
{
    public class AboutRepo : IAboutRepo
    {
        private readonly CalligraphyContext _context;

        public AboutRepo(CalligraphyContext context)
        {
            _context = context;
        }

        public AboutRepo()
        {
            _context = new CalligraphyContext();
        }

        public AboutEntity Get()
        {
            return _context.About.FirstOrDefault();
        }

        public AboutEntity Update(AboutEntity about)
        {
            _context.Entry(about).State = EntityState.Modified;
            _context.SaveChanges();
            return about;
        }
    }
}