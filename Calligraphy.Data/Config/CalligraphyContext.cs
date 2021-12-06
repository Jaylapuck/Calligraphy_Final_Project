using Calligraphy.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calligraphy.Data.Config
{
    public class CalligraphyContext : DbContext
    {
        public CalligraphyContext() : base("CalligraphyContext")
        {
        }

        public DbSet<FormEntity> Forms { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FormEntity>().ToTable("Form");
        }
    }
}
