using Calligraphy.Data.Config;
using Calligraphy.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Calligraphy.Data.Repo.Service
{
    public class ServiceRepoImpl : IServiceRepo
    {
        private readonly CalligraphyContext _context;
        private readonly DbContextOptions<CalligraphyContext> options;


        public ServiceRepoImpl(CalligraphyContext context)
        {
            _context = context;
        }

        public ServiceRepoImpl()
        {
            _context = new CalligraphyContext(options);
        }

        public IEnumerable<ServiceEntity> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
