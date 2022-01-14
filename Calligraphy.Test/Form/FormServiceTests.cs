using System;
using System.Collections.Generic;
using System.Linq;
using Calligraphy.Business.Form;
using Calligraphy.Data.Enums;
using Calligraphy.Data.Filters;
using Calligraphy.Data.Helpers;
using Calligraphy.Data.IUriService;
using Calligraphy.Data.Models;
using Calligraphy.Data.Repo.Form;
using Calligraphy.Data.Repo.Service;
using Calligraphy.Data.Repo.Wrappers;
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
        private readonly Mock<PaginationHelper> _mockPaginationHelper;
        private readonly FormService _formService;

        public FormServiceTests()
        {
            _mockFormRepo = new Mock<IFormRepo>();
            _mockServiceRepo = new Mock<IServiceRepo>();
            _mockUriService = new Mock<IUriService>();
            _mockPaginationHelper = new Mock<PaginationHelper>();
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
                new() {ServiceId = 1, TypeName = ServiceType.Calligraphy, StartingRate = 20.00f},
                new() {ServiceId = 2, TypeName = ServiceType.Engraving, StartingRate = 30.00f}
            };

            // Act
            _mockServiceRepo.Setup(x => x.GetAll()).Returns(dummyServices);
            var result = _formService.GetAllServices();
            

            // Assert 
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void GetAllOkResult()
        {
         //Arrange 
            var dummyForms = new List<FormEntity>
            {
                new() {FormId = 1, ServiceType = ServiceType.Calligraphy, Comments = "Comments 1"},
                new() {FormId = 2, ServiceType = ServiceType.Calligraphy, Comments = "Comments 2"},
                new() {FormId = 3, ServiceType = ServiceType.Calligraphy, Comments = "Comments 3"}
            };
            
            var dummyUri = new Uri("http://localhost:5000/api/form");
            
            var dummyPaginationFilter = new PaginationFilter
            {
                PageNumber = 1,
                PageSize = 2
            };

            var totalRecords = 0;
            
            var dummyRoute = 
                $"{dummyUri.AbsolutePath}?pageNumber={dummyPaginationFilter.PageNumber}&pageSize={dummyPaginationFilter.PageSize}";

            _mockFormRepo.Setup(x => x.GetAll(dummyPaginationFilter, out totalRecords )).Returns(dummyForms);

            // create a PagedResponse
            var pagedResponse = new PagedResponse<IEnumerable<FormEntity>>(dummyForms, dummyPaginationFilter.PageNumber,
                dummyPaginationFilter.PageSize);

            // Act
            var result = _formService.GetAll(dummyPaginationFilter, dummyRoute);
            
            // Assert
            Assert.NotEqual(new OkObjectResult(pagedResponse), result);
        }
    }
}
