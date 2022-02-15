using System;
using Calligraphy.Data.Enums;
using Calligraphy.Data.Models;
using Calligraphy.Data.Models.AuthenticationModels.JWT;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Calligraphy.Data.Config
{
    public class CalligraphyContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public CalligraphyContext(DbContextOptions<CalligraphyContext> options) : base(options)
        {
        }
        
        public CalligraphyContext()
        {
            _configuration = new ConfigurationBuilder()
                .Build();
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
                Password = BCrypt.Net.BCrypt.HashPassword("admin", BCrypt.Net.BCrypt.GenerateSalt())
            });

            builder.Entity<AboutEntity>().HasData(new AboutEntity
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

            builder.Entity<ServiceEntity>().HasData(new ServiceEntity
            {
                ServiceId = 1,
                StartingRate = 20.0f,
                TypeName = ServiceType.Engraving
            });

            builder.Entity<ServiceEntity>().HasData(new ServiceEntity
            {
                ServiceId = 2,
                StartingRate = 30.0f,
                TypeName = ServiceType.Calligraphy
            });

            //build five form entries
            builder.Entity<FormEntity>().HasData(new FormEntity
            {
                FormId = 1,
                CreatedDate = DateTime.Now,
                Comments = "I am a student, worked here blabla"
            });

            builder.Entity<FormEntity>().HasData(new FormEntity
            {
                FormId = 2,
                CreatedDate = DateTime.Now,
                Comments = "I am a student, worked here blabla"
            });

            builder.Entity<FormEntity>().HasData(new FormEntity
            {
                FormId = 3,
                CreatedDate = DateTime.Now,
                Comments = "I am a student, worked here blabla"
            });

            builder.Entity<FormEntity>().HasData(new FormEntity
            {
                FormId = 4,
                CreatedDate = DateTime.Now,
                Comments = "I am a student, worked here blabla"
            });

            builder.Entity<FormEntity>().HasData(new FormEntity
            {
                FormId = 5,
                CreatedDate = DateTime.Now,
                Comments = "I am a student, worked here blabla"
            });
        }
    }
}