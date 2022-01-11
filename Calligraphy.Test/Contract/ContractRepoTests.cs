using Calligraphy.Data.Config;
using Calligraphy.Data.Models;
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
                Assert.Equal(4, result.Count());
            }
        }

        [Fact]
        // Test to see if we can retrieve a single contract
        public void GetSingleContractOk()
        {
            using (var context = new CalligraphyContext(ContextOptions))
            {
                // Arrange 
                var contextRepo = new ContractRepo(context);

                // Act
                var result = contextRepo.GetById(4);

                // Assert
                Assert.IsType<ContractEntity>(result);
                Assert.Equal(4, result.ContractId);
            }
        }

        [Fact]
        // Test to see if we can create a new contract
        public void CreateNewContractOk() 
        { 
            using (var context = new CalligraphyContext(ContextOptions))
            {
                // Arrange 
                var contextRepo = new ContractRepo(context);
                ContractEntity dummyEntity = new ContractEntity { FinalCost = 60.00, DownPayment = 30.00, DateCommissioned = new DateTime(2021, 11, 1), EndDate = new DateTime(2021, 11, 20), HasSignature = true, IsFinished = true };

                // Act
                var result = contextRepo.CreateNewContract(dummyEntity);

                // Assert
                Assert.Equal(1, result);
            }
        }
    }
}
