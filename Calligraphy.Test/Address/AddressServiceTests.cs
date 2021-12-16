using Calligraphy.Business.Address;
using Calligraphy.Data.Models;
using Calligraphy.Data.Repo.Address;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Calligraphy.Test.Address
{
    public class AddressServiceTests
    {
        private readonly Mock<IAddressRepo> _mockAddressRepo;
        private readonly AddressService _addressService;

        public string street1 = "Street1", postal1 = "postal1", city1 = "city1", country1 = "Country1";
        public string street2 = "Street2", postal2 = "postal2", city2 = "city2", country2 = "Country2";
        public string street3 = "Street3", postal3 = "postal3", city3 = "city3", country3 = "Country3";


        public AddressServiceTests()
        {
            _mockAddressRepo = new Mock<IAddressRepo>();
            _addressService = new AddressService(_mockAddressRepo.Object);
        }

        [Fact]
        // TS2-TC5
        public void GetAllAddresses()
        {
            // Arrange
            var address = new List<AddressEntity>
            {
                new AddressEntity { AddressId = 1, Street = street1, Postal=postal1, City=city1},
                new AddressEntity { AddressId = 2, Street = street2, Postal=postal2, City=city2},
                new AddressEntity { AddressId = 3, Street = street3, Postal=postal3, City=city3},

            };

            // Act
            _mockAddressRepo.Setup(x => x.GetAll()).Returns(address);
            var result = _addressService.GetAll();

            // Assert
            Assert.Equal(3, result.Count());

        }

        [Fact]
        // TS1-TC6
        public void CreateAddress()
        {
            // Arrange
            var address = new AddressEntity { AddressId = 1, Street = street1, Postal = postal1, City = city1 };

            // Act
            _mockAddressRepo.Setup(x => x.Create(address));
            _addressService.Create(address);

            // Assert
            _mockAddressRepo.Verify(x => x.Create(address), Times.Once);
        }
    }
}