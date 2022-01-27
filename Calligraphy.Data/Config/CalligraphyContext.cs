using Calligraphy.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calligraphy.Data.Enums;
using Calligraphy.Data.Models.AuthenticationModels;
using Calligraphy.Data.Models.AuthenticationModels.JWT;

namespace Calligraphy.Data.Config
{
    public class CalligraphyContext : DbContext
    {
        public CalligraphyContext(DbContextOptions<CalligraphyContext> options) : base(options)
        {
        }

        public DbSet<FormEntity> Forms { get; set; }
        public DbSet<QuoteEntity> Quotes { get; set; }
        public DbSet<ImageEntity> Images { get; set; }
        public DbSet<CustomerEntity> Customers { get; set; }
        public DbSet<AddressEntity> Addresses { get; set; }
        public DbSet<ServiceEntity> Services { get; set; }
        public DbSet<ContractEntity> Contracts { get; set; }
        public DbSet<AdminEntity> Admins { get; set; }
        
        public DbSet<AboutEntity> About { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AdminEntity>(admin => { admin.HasIndex(e => e.UserName).IsUnique(); });

            builder.Entity<AdminEntity>().HasData(new AdminEntity
            {
                Id = 1,
                UserName = "admin",
                Password = "admin"
            });

            builder.Entity<AboutEntity>().HasData(new AboutEntity()
            {
                AboutId = 1,
                Name = "Serena Tam",
                Email = "serena22@email.com",
                Phone = "(123)-456-7890",
                Profession = "Calligrapher and Engraver",
                Description = "I am a student, worked here blabla",
                Language = "All Languages",
                Country = "Canada",
                Experience = "Phd and masters",
                Mission = "Make that moneeeeeyy ya know"
            });
            
            //create two entries data for serviceEntity

            builder.Entity<ServiceEntity>().HasData(new ServiceEntity()
            {
                ServiceId = 1,
                StartingRate = 20.0f,
                TypeName = ServiceType.Engraving
            });
            
            builder.Entity<ServiceEntity>().HasData(new ServiceEntity()
            {
                ServiceId = 2,
                StartingRate = 30.0f,
                TypeName = ServiceType.Calligraphy
            });
            
        }
    }
}
