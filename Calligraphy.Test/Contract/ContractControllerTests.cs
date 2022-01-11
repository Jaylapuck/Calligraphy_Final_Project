﻿using Calligraphy.Business.Contract;
using Calligraphy.Controllers;
using Calligraphy.Data.Models;
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
    public class ContractControllerTests
    {
        private Mock<IContractService> _mockContractService;
        private ContractController _contractController;

        public ContractControllerTests()
        {
            _mockContractService = new Mock<IContractService>();
            _contractController = new ContractController(_mockContractService.Object);
        }

        [Fact]
        // Test to see if we get a good result when fetching all contracts and getting back some entries
        public void GetAllContractsOkResultReturnsList()
        {
            // Arrange
            List<ContractEntity> contracts = new List<ContractEntity>
            {
                new ContractEntity { FinalCost = 150.00, DownPayment = 75.00, DateCommissioned = new DateTime(2021, 6, 8), EndDate = new DateTime(2021, 7, 8), HasSignature = true, IsFinished = true },
                new ContractEntity { FinalCost = 60.00, DownPayment = 30.00, DateCommissioned = new DateTime(2017, 5, 19), EndDate = new DateTime(2017, 5, 30), HasSignature = true, IsFinished = true},
                new ContractEntity { FinalCost = 200.00, DownPayment = 100.00, DateCommissioned = new DateTime(2021, 10, 8), EndDate = new DateTime(2022, 2, 27), HasSignature = true, IsFinished = false},
                new ContractEntity { FinalCost = 0.0, DownPayment = 0.0, DateCommissioned = new DateTime(2022, 1, 10), EndDate = new DateTime(2022, 3, 21), HasSignature = false, IsFinished = false}
            };

            _mockContractService.Setup(x => x.GetAllContracts()).Returns(new OkObjectResult(contracts));

            // Act
            var result = _contractController.GetAllContracts();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        // Test to see if we get a good result when fetching all contracts and getting back an empty list
        public void GetAllContractsOkResultReturnsEmptyList()
        {
            // Arrange
            List<ContractEntity> contracts = new List<ContractEntity>();

            _mockContractService.Setup(x => x.GetAllContracts()).Returns(new OkObjectResult(contracts));

            // Act
            var result = _contractController.GetAllContracts();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        // Test to see if we get a bad result
        public void GetAllContractsBadRequest()
        {
            // Arrange
            _mockContractService.Setup(x => x.GetAllContracts()).Returns(new BadRequestResult());

            // Act
            var result = _contractController.GetAllContracts();

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }
    }
}
