using Calligraphy.Business.Customer;
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

namespace Calligraphy.Test.Customer
{
    public class CustomerControllerTests
    {
        private readonly Mock<ICustomerService> _mockCustomerService;
        private readonly CustomerController _customerController;

        public string firstName1 = "John1", lastName1 = "Doe1", email1 = "email1@email.com";
        public string firstName2 = "John2", lastName2 = "Doe2", email2 = "email2@email.com";

        public CustomerControllerTests()
        {
            _mockCustomerService = new Mock<ICustomerService>();
            _customerController = new CustomerController(_mockCustomerService.Object);
        }

        // TS2-TC1
        [Fact]
        // test get all api, returns list of customers
        public void GetAll()
        {
            // Arrange
            var customers = new List<CustomerEntity>
            {
                new CustomerEntity { CustomerId = 1, FirstName = firstName1, LastName=lastName1, Email=email1},
                new CustomerEntity { CustomerId = 2, FirstName = firstName2, LastName=lastName2, Email=email2}
            };

            _mockCustomerService.Setup(x => x.GetAll()).Returns(customers);

            // Act
            var result = _customerController.Get();

            // Assert
            Assert.IsType<List<CustomerEntity>>(result);
            Assert.Equal(2, result.Count());
        }

        // TS2-TC3
        [Fact]
        // test get all api, returns empty list
        public void GetAll_ReturnsEmptyList()
        {
            // Arrange
            var customers = new List<CustomerEntity>();

            _mockCustomerService.Setup(x => x.GetAll()).Returns(customers);

            // Act
            var result = _customerController.Get();

            // Assert
            Assert.IsType<List<CustomerEntity>>(result);
            Assert.Empty(result);
        }

        [Fact]
        // TC2-TC2
        public void Post_ReturnsOkResult()
        {
            // Arrange
            var expected = new CustomerEntity();
            _mockCustomerService.Setup(x => x.Create(expected)).Returns(true);

            // Act
            var actual = _customerController.Post(expected);

            // Assert
            Assert.IsType<OkObjectResult>(actual);
        }

        [Fact]
        // TC2-TC4
        public void Post_ReturnsBadRequest()
        {
            // Arrange
            var expected = new CustomerEntity();
            _mockCustomerService.Setup(x => x.Create(expected)).Returns(false);

            // Act
            var actual = _customerController.Post(null);

            // Assert
            Assert.IsType<BadRequestResult>(actual);
        }
    }
}
