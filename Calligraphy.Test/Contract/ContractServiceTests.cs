using Calligraphy.Business.Contract;
using Calligraphy.Data.Models;
using Calligraphy.Data.Repo.Contract;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Calligraphy.Test.Contract
{
    public class ContractServiceTests
    {
        private Mock<IContractRepo> _mockContractRepo;
        private ContractService _contractService;

        public ContractServiceTests()
        {
            _mockContractRepo = new Mock<IContractRepo>();
            _contractService = new ContractService(_mockContractRepo.Object);
        }

        [Fact]
        // Test to see if we can get back all the contracts
        public void GetAllContractsOk()
        {
            // Arrange
            List<ContractEntity> contracts = new List<ContractEntity>
            {
                new ContractEntity { FinalCost = 150.00, DownPayment = 75.00, DateCommissioned = new DateTime(2021, 6, 8), EndDate = new DateTime(2021, 7, 8), HasSignature = true, IsFinished = true },
                new ContractEntity { FinalCost = 60.00, DownPayment = 30.00, DateCommissioned = new DateTime(2017, 5, 19), EndDate = new DateTime(2017, 5, 30), HasSignature = true, IsFinished = true},
                new ContractEntity { FinalCost = 200.00, DownPayment = 100.00, DateCommissioned = new DateTime(2021, 10, 8), EndDate = new DateTime(2022, 2, 27), HasSignature = true, IsFinished = false},
                new ContractEntity { FinalCost = 0.0, DownPayment = 0.0, DateCommissioned = new DateTime(2022, 1, 10), EndDate = new DateTime(2022, 3, 21), HasSignature = false, IsFinished = false}
            };

            _mockContractRepo.Setup(x => x.GetAll()).Returns(contracts);

            // Act
            var result = _contractService.GetAllContracts();

            // Assert
            Assert.IsType<ContractEntity>(result.ElementAt(0));
            Assert.Equal(4, result.Count());
        }
    }
}
