using System;
using System.Collections.Generic;
using System.Linq;
using Calligraphy.Business.Form;
using Calligraphy.Data.Enums;
using Calligraphy.Data.Models;
using Calligraphy.Data.Pagination;
using Calligraphy.Data.Repo.Form;
using Calligraphy.Data.Repo.Service;
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
        //TC4-TS1
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

        //TC4-TS2
        [Fact]
        public void GetAllServicesOk()
        {
            // Arrange
            List<ServiceEntity> dummyServices = new List<ServiceEntity>
            {
                new() {ServiceId = 1, TypeName = ServiceType.Calligraphy, StartingRate = 20.00f},
                new() {ServiceId = 2, TypeName = ServiceType.Engraving, StartingRate = 30.00f}
            };

            // Act
            _mockServiceRepo.Setup(x => x.GetAll()).Returns(dummyServices);
            var result = _formService.GetAllServices();
            

            // Assert 
            Assert.Equal(2, result.Count());
        }

        //TC4-TS3
        [Fact]
        public void GetAllServicesEmpty()
        {
            // Arrange
            List<ServiceEntity> dummyServices = new List<ServiceEntity>();

            // Act
            _mockServiceRepo.Setup(x => x.GetAll()).Returns(dummyServices);
            var result = _formService.GetAllServices();

            // Assert 
            Assert.Empty(result);
        }
        
        //TC4-TS4
        [Fact]
        public void GetAllServicesNull()
        {
            // Act
            _mockServiceRepo.Setup(x => x.GetAll()).Returns((List<ServiceEntity>) null);
            var result = _formService.GetAllServices();

            // Assert 
            Assert.Null(result);
        }
        
        //TC4-TS5
        [Fact]
        public void GetAllServicesException()
        {
            // Arrange

            // Act
            _mockServiceRepo.Setup(x => x.GetAll()).Throws(new Exception());
            var result = _formService.GetAllServices();

            // Assert 
            Assert.Null(result);
        }
        
        //TC4-TS6
        [Fact]
        public void GetAllForms()
        {
            // Arrange
            var dummyForms = new List<FormEntity>
            {
                new() {FormId = 1, ServiceType = ServiceType.Calligraphy, Comments = "Comments 1"},
                new() {FormId = 2, ServiceType = ServiceType.Engraving, Comments = "Comments 2"}
            };
            
            var formParams = new FormParameters()
            {
                PageNumber = 1,
                PageSize = 2,
            };
            
            var paged = new PagedList<FormEntity>(dummyForms, 10, formParams.PageNumber, formParams.PageSize);
            
            // Act
            _mockFormRepo.Setup(x => x.GetAll(formParams)).Returns(paged);
            var result = _formService.GetAll(formParams);
            
            // Assert 
            Assert.Equal(2, result.Count());
            Assert.Equal(1, result.First().FormId);
            Assert.Equal(2, result.Last().FormId);
        }
    }
}
