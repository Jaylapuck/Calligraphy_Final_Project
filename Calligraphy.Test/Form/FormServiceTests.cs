using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calligraphy.Business.Form;
using Calligraphy.Controllers;
using Calligraphy.Data.Enums;
using Calligraphy.Data.Models;
using Calligraphy.Data.Repo;
using Calligraphy.Data.Repo.Service;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Calligraphy.Test.Form
{
    public class FormServiceTests
    {
        private readonly Mock<IFormRepo> _mockFormRepo;
        private readonly Mock<IServiceRepo> _mockServiceRepo;
        private readonly FormService _formService;

        public FormServiceTests()
        {
            _mockFormRepo = new Mock<IFormRepo>();
            _mockServiceRepo = new Mock<IServiceRepo>();
            _formService = new FormService(_mockFormRepo.Object, _mockServiceRepo.Object);
        }

        [Fact]
        // TS2-TC5
        public void GetAllForms()
        {
            // Arrange
            AddressEntity dummyAddress = new AddressEntity { AddressId = 1, Street = "some street", City = "some city", Country = "some country", Postal = "some code" };
            CustomerEntity dummyCustomer = new CustomerEntity { CustomerId = 1, FirstName = "some name", LastName = "some name", Address = dummyAddress, Email = "some email" };
            var forms = new List<FormEntity>
            {
                new FormEntity {FormId = 1, Customer = dummyCustomer, ServiceType = ServiceType.Calligraphy, StartingRate = 20.00, Comments = "Comments 1"},
                new FormEntity {FormId = 2, Customer = dummyCustomer, ServiceType = ServiceType.Engraving, StartingRate = 30.00, Comments = "Comments 2"}
            };
            
            // Act
            _mockFormRepo.Setup(x => x.GetAll()).Returns(forms);
            var result = _formService.GetAll();
            
            // Assert
            Assert.Equal(2, result.Count());
            foreach(FormEntity temp in result)
            {
                Assert.NotNull(temp.Customer);
            }
        }
        
        [Fact]
        // TS1-TC6
        public void CreateForm()
        {
            // Arrange
            var form = new FormEntity {FormId = 1, ServiceType = ServiceType.Calligraphy, Comments = "Comments 1"};
            
            // Act
            _mockFormRepo.Setup(x => x.Create(form));
            _formService.Create(form);
            
            // Assert
            _mockFormRepo.Verify(x => x.Create(form), Times.Once);
        }

        [Fact]
        public void GetAllServicesOk()
        {
            // Arrange
            List<ServiceEntity> dummyServices = new List<ServiceEntity>
            {
                new ServiceEntity{ServiceId = 1, TypeName = ServiceType.Calligraphy, StartingRate = 20.00f},
                new ServiceEntity{ServiceId = 2, TypeName = ServiceType.Engraving, StartingRate = 30.00f}
            };

            // Act
            _mockServiceRepo.Setup(x => x.GetAll()).Returns(dummyServices);
            var result = _formService.GetAllServices();

            // Assert 
            Assert.Equal(2, result.Count());
        }
    }
}
