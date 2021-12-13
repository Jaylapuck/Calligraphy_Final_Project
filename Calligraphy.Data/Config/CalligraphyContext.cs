using Calligraphy.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calligraphy.Data.Config
{
    public class CalligraphyContext : DbContext
    {
        public CalligraphyContext(DbContextOptions<CalligraphyContext> options) : base(options)
        {
        }

        public DbSet<FormEntity> Forms { get; set; }
        public DbSet<AddressEntity> Address { get; set; }
    }
}
