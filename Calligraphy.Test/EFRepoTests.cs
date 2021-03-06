using System;
using System.Collections.Generic;
using Calligraphy.Data.Config;
using Calligraphy.Data.Enums;
using Calligraphy.Data.Models;
using Castle.Core.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Calligraphy.Test
{
    public class EfRepoTests
    {
        private readonly IConfiguration _configuration;
        protected EfRepoTests(DbContextOptions<CalligraphyContext> contextOptions, IConfiguration configuration)
        {
            ContextOptions = contextOptions;
            _configuration = configuration;
            Seed();
        }

        protected EfRepoTests(DbContextOptions<CalligraphyContext> contextOptions)
        {
            ContextOptions = contextOptions;
            Seed();
        }

        protected DbContextOptions<CalligraphyContext> ContextOptions { get; }

        private void Seed()
        {
            using var context = new CalligraphyContext(ContextOptions);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
          
            var contracts = new List<ContractEntity>
            {
                new()
                {
                    FinalCost = 150.00, DownPayment = 75.00, DateCommissioned = new DateTime(2021, 6, 8),
                    EndDate = new DateTime(2021, 7, 8), HasSignature = true, IsFinished = true
                },
                new()
                {
                    FinalCost = 150.00, DownPayment = 75.00, DateCommissioned = new DateTime(2021, 6, 10),
                    EndDate = new DateTime(2021, 6, 30), HasSignature = true, IsFinished = true
                },
                new()
                {
                    FinalCost = 60.00, DownPayment = 30.00, DateCommissioned = new DateTime(2017, 5, 19),
                    EndDate = new DateTime(2017, 5, 30), HasSignature = true, IsFinished = true
                },
                new()
                {
                    FinalCost = 200.00, DownPayment = 100.00, DateCommissioned = new DateTime(2021, 10, 8),
                    EndDate = new DateTime(2022, 2, 27), HasSignature = true, IsFinished = false
                },
                new()
                {
                    FinalCost = 0.0, DownPayment = 0.0, DateCommissioned = new DateTime(2022, 1, 10),
                    EndDate = new DateTime(2022, 3, 21), HasSignature = false, IsFinished = false
                }
            };

            context.AddRange(contracts);
            context.SaveChanges();
        }
    }
}