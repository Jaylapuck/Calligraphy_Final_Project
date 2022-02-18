using System.Collections.Generic;
using System.Linq;
using Calligraphy.Data.Config;
using Calligraphy.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Calligraphy.Data.Repo.Service
{
    public class ServiceRepoImpl : IServiceRepo
    {
        private readonly CalligraphyContext _context;


        public ServiceRepoImpl(CalligraphyContext context)
        {
            _context = context;
        }

        public IEnumerable<ServiceEntity> GetAll()
        {
            using (_context)
            {
                return _context.Services.ToList();
            }
        }
    }
}