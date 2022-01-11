using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calligraphy.Business.Form;
using Calligraphy.Controllers;
using Calligraphy.Data.Enums;
using Calligraphy.Data.IUriService;
using Calligraphy.Data.Models;
using Calligraphy.Data.Repo;
using Calligraphy.Data.Repo.Form;
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
        private readonly Mock<IUriService> _mockUriService;
        private readonly FormService _formService;

        public FormServiceTests()
        {
            _mockFormRepo = new Mock<IFormRepo>();
            _mockServiceRepo = new Mock<IServiceRepo>();
            _mockUriService = new Mock<IUriService>();
            _formService = new FormService(_mockFormRepo.Object, _mockServiceRepo.Object, _mockUriService.Object);
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
