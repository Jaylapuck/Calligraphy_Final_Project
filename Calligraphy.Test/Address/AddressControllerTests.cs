using Calligraphy.Business.Address;
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

namespace Calligraphy.Test.Address
{
    public class AddressControllerTests
    {
        private readonly Mock<IAddressService> _mockAddressService;
        private readonly AddressController _addressController;

        public string street1 = "Street1", postal1 = "postal1", city1 = "city1", country1 = "Country1";
        public string street2 = "Street2", postal2 = "postal2", city2 = "city2", country2 = "Country2";

        public AddressControllerTests()
        {
            _mockAddressService = new Mock<IAddressService>();
            _addressController = new AddressController(_mockAddressService.Object);
        }

        // TC1-TA1
        [Fact]
        // test get all api, returns list of addresses
        public void GetAll()
        {
            // Arrange
            var addresses = new List<AddressEntity>
            {
                new AddressEntity { AddressId = 1, Street = street1, Postal=postal1, City=city1},
                new AddressEntity { AddressId = 2, Street = street2, Postal=postal2, City=city2}
            };

            _mockAddressService.Setup(x => x.GetAll()).Returns(addresses);

            // Act
            var result = _addressController.Get();

            // Assert
            Assert.IsType<List<AddressEntity>>(result);
            Assert.Equal(2, result.Count());
        }

        // TC1-TA2
        [Fact]
        // test get all api, returns empty list
        public void GetAll_ReturnsEmptyList()
        {
            // Arrange
            var addresses = new List<AddressEntity>();

            _mockAddressService.Setup(x => x.GetAll()).Returns(addresses);

            // Act
            var result = _addressController.Get();

            // Assert
            Assert.IsType<List<AddressEntity>>(result);
            Assert.Empty(result);
        }

        [Fact]
        // TC1-TA3
        public void Post_ReturnsOkResult()
        {
            // Arrange
            var expected = new AddressEntity();
            _mockAddressService.Setup(x => x.Create(expected)).Returns(true);

            // Act
            var actual = _addressController.Post(expected);

            // Assert
            Assert.IsType<OkObjectResult>(actual);
        }

        [Fact]
        // TC1-TA4
        public void Post_ReturnsBadRequest()
        {
            // Arrange
            var expected = new AddressEntity();
            _mockAddressService.Setup(x => x.Create(expected)).Returns(false);

            // Act
            var actual = _addressController.Post(null);

            // Assert
            Assert.IsType<BadRequestResult>(actual);
        }
    }
}
