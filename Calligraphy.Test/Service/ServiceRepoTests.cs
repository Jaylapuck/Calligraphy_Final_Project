using Calligraphy.Data.Config;
using Calligraphy.Data.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;
using System.Reflection;
using System.Globalization;
using System.Threading;
using Calligraphy.Data.Repo.Service;

namespace Calligraphy.Test.Service
{
    public class ServiceRepoTests : EfRepoTests
    {
        public ServiceRepoTests() : base(
            new DbContextOptionsBuilder<CalligraphyContext>()
            .UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Test_FP_DB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
            .Options)
        {

        }

        //TC8-TR1
        [Fact]
        public void GetAllServicesOk()
        {
            using (var context = new CalligraphyContext(ContextOptions))
            {
                // Arrange
                var serviceRepo = new ServiceRepoImpl(context);

                // Act
                var result = serviceRepo.GetAll();

                // Assert
                Assert.Single(result);
            }
        }
    }
}
