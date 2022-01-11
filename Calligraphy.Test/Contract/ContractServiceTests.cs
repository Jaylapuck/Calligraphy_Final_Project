﻿using Calligraphy.Business.Contract;
using Calligraphy.Data.Models;
using Calligraphy.Data.Repo.Contract;
using Microsoft.AspNetCore.Mvc;
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
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        // Test to see if we get back a not found result
        public void GetAllContractsNotFound()
        {
            // Arrange
            _mockContractRepo.Setup(x => x.GetAll()).Returns((List<ContractEntity>) null);

            // Act
            var result = _contractService.GetAllContracts();

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        // Test to see if we get a single contract by it's id
        public void GetContractByIdOk()
        {
            // Arrange 
            ContractEntity contract = new ContractEntity { ContractId = 1, FinalCost = 150.00, DownPayment = 75.00, DateCommissioned = new DateTime(2021, 6, 8), EndDate = new DateTime(2021, 7, 8), HasSignature = true, IsFinished = true };

            _mockContractRepo.Setup(x => x.GetById(It.IsAny<int>())).Returns(contract);

            // Act
            var result = _contractService.GetContractById(1);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        // Test to see if we get a not found result trying to get a contract by id
        public void GetContractByIdNotFound()
        {
            // Arrange 
            _mockContractRepo.Setup(x => x.GetById(It.IsAny<int>())).Returns((ContractEntity) null);

            // Act
            var result = _contractService.GetContractById(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        // Test to see if we can create a single contract
        public void CreateContractTestOk()
        {
            // Arrange
            ContractEntity contract = new ContractEntity { FinalCost = 150.00, DownPayment = 75.00, DateCommissioned = new DateTime(2021, 6, 8), EndDate = new DateTime(2021, 7, 8), HasSignature = true, IsFinished = true };

            _mockContractRepo.Setup(x => x.CreateNewContract(contract)).Returns(1);

            // Act
            var result = _contractService.CreateNewContract(contract);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        // Test to see if we get a bad request when making a contract
        public void CreateContractTestBadrequest()
        {
            // Arrange
            ContractEntity contract = new ContractEntity { FinalCost = 150.00, DownPayment = 75.00, DateCommissioned = new DateTime(2021, 6, 8), EndDate = new DateTime(2021, 7, 8), HasSignature = true, IsFinished = true };

            _mockContractRepo.Setup(x => x.CreateNewContract(contract)).Returns(0);

            // Act
            var result = _contractService.CreateNewContract(contract);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }
    }
}
