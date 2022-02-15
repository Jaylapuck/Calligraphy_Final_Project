using Calligraphy.Data.Config;
using Calligraphy.Data.Repo.Service;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Calligraphy.Test.Service
{
    public class ServiceRepoTests : EfRepoTests
    {
        public ServiceRepoTests() : base(
            new DbContextOptionsBuilder<CalligraphyContext>()
                .UseSqlServer(
                    "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Test_FP_DB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
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