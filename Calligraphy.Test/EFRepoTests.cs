using Calligraphy.Data.Config;
using Calligraphy.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calligraphy.Test
{
    public class EFRepoTests
    {
        protected DbContextOptions<CalligraphyContext> ContextOptions { get; }

        public EFRepoTests(DbContextOptions<CalligraphyContext> contextOptions)
        {
            ContextOptions = contextOptions;

            Seed();
        }

        private void Seed()
        {
            using (var context = new CalligraphyContext(ContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var service1 = new ServiceEntity { TypeName = Data.Enums.ServiceType.Calligraphy, StartingRate = 20.00f };

                context.AddRange(service1);

                context.SaveChanges();
            }
        }
    }
}
