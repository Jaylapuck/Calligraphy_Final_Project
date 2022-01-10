using Calligraphy.Data.Config;
using Calligraphy.Data.Repo.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Calligraphy.Test.Contract
{
    public class ContractRepoTests : EFRepoTests
    {
        public ContractRepoTests() : base(
            new DbContextOptionsBuilder<CalligraphyContext>()
            .UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Test_FP_DB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
            .Options)
        {

        }

        [Fact]
        // Test to see if we can recover all registered contracts
        public void GetAllContractsOk()
        {
            using (var context = new CalligraphyContext(ContextOptions))
            {
                // Arrange
                var contractRepo = new ContractRepo(context);

                // Act
                var result = contractRepo.GetAll();

                // Assert
                Assert.Single(result);
            }
        }
    }
}
