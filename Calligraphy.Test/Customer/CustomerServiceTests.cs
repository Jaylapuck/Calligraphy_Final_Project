using Calligraphy.Business.Customer;
using Calligraphy.Data.Models;
using Calligraphy.Data.Repo.Customer;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Calligraphy.Test.Customer
{
    public class CustomerServiceTests
    {
        private readonly Mock<ICustomerRepo> _mockCustomerRepo;
        private readonly CustomerService _customerService;

        public string firstName1 = "John1", lastName1 = "Doe1", email1 = "email1@email.com";
        public string firstName2 = "John2", lastName2 = "Doe2", email2 = "email2@email.com";
        public string firstName3 = "John3", lastName3 = "Doe3", email3 = "email3@email.com";


        public CustomerServiceTests()
        {
            _mockCustomerRepo = new Mock<ICustomerRepo>();
            _customerService = new CustomerService(_mockCustomerRepo.Object);
        }

        [Fact]
        // TC3-TS1
        public void GetAllCustomers()
        {
            // Arrange
            var customers = new List<CustomerEntity>
            {
                new CustomerEntity { CustomerId = 1, FirstName = firstName1, LastName=lastName1, Email=email1},
                new CustomerEntity { CustomerId = 2, FirstName = firstName2, LastName=lastName2, Email=email2},
                new CustomerEntity { CustomerId = 3, FirstName = firstName3, LastName=lastName3, Email=email3},

            };

            // Act
            _mockCustomerRepo.Setup(x => x.GetAll()).Returns(customers);
            var result = _customerService.GetAll();

            // Assert
            Assert.Equal(3, result.Count());

        }

        [Fact]
        // TC3-TS2
        public void CreateCustomer()
        {
            // Arrange
            var customer = new CustomerEntity { CustomerId = 1, FirstName = firstName1, LastName = lastName1, Email = email1 };

            // Act
            _mockCustomerRepo.Setup(x => x.Create(customer));
            _customerService.Create(customer);

            // Assert
            _mockCustomerRepo.Verify(x => x.Create(customer), Times.Once);
        }
    }
}
