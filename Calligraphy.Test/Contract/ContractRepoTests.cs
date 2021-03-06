using System;
using System.Linq;
using Calligraphy.Data.Config;
using Calligraphy.Data.Models;
using Calligraphy.Data.Repo.Contract;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Calligraphy.Test.Contract
{
    public class ContractRepoTests : EfRepoTests
    {
        public ContractRepoTests() : base(
            new DbContextOptionsBuilder<CalligraphyContext>()
                .UseSqlServer(
                    "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Test_FP_DB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
                .Options)
        {
        }

        [Fact]
        // TC2-TR1
        // Test to see if we can recover all registered contracts
        public void GetAllContractsOk()
        {
            using var context = new CalligraphyContext(ContextOptions);
            // Arrange
            var contractRepo = new ContractRepo(context);

            // Act
            var result = contractRepo.GetAll();

            // Assert
            Assert.Equal(5, result.Count());
        }

        [Fact]
        // TC2-TR2
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
        // TC2-TR3
        // Test to see if we can create a new contract
        public void CreateNewContractOk()
        {
            using (var context = new CalligraphyContext(ContextOptions))
            {
                // Arrange 
                var contextRepo = new ContractRepo(context);
                var dummyEntity = new ContractEntity
                {
                    FinalCost = 60.00, DownPayment = 30.00, DateCommissioned = new DateTime(2021, 11, 1),
                    EndDate = new DateTime(2021, 11, 20), HasSignature = true, IsFinished = true
                };

                // Act
                var result = contextRepo.CreateNewContract(dummyEntity);

                // Assert
                Assert.Equal(1, result);
            }
        }

        [Fact]
        // TC2-TR4
        // Test to see if we can update a given contract 
        public void UpdateExistingContractOk()
        {
            using var context = new CalligraphyContext(ContextOptions);
            // Arrange 
            var contextRepo = new ContractRepo(context);
            var dummyEntity = new ContractEntity
            {
                ContractId = 4, FinalCost = 100.00, DownPayment = 50.00, DateCommissioned = new DateTime(2022, 1, 10),
                EndDate = new DateTime(2022, 3, 21), HasSignature = true, IsFinished = true
            };

            // Act
            var result = contextRepo.UpdateContract(dummyEntity);

            // Assert
            Assert.IsType<ContractEntity>(result);
            Assert.Equal(4, result.ContractId);
            Assert.NotEqual(0.0, result.FinalCost);
            Assert.NotEqual(0.0, result.DownPayment);
            Assert.True(result.HasSignature);
            Assert.True(result.IsFinished);
        }

        // TC2-TR5
        [Fact]
        // Test to see if we can recover contracts for a specific month that are done
        public void GetContractsByMonthOk()
        {
            using var context = new CalligraphyContext(ContextOptions);
            // Arrange
            var contractRepo = new ContractRepo(context);

            // Act
            var result = contractRepo.GetByMonthOfYear(6, 2021, true);

            // Assert
            Assert.Equal(2, result.Count());
        }
    }
}