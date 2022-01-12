using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calligraphy.Business.Form;
using Calligraphy.Controllers;
using Calligraphy.Data.Enums;
using Calligraphy.Data.Filters;
using Calligraphy.Data.Helpers;
using Calligraphy.Data.IUriService;
using Calligraphy.Data.Models;
using Calligraphy.Data.Repo;
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
                new ServiceEntity{ServiceId = 1, TypeName = ServiceType.Calligraphy, StartingRate = 20.00f},
                new ServiceEntity{ServiceId = 2, TypeName = ServiceType.Engraving, StartingRate = 30.00f}
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
            // This will be done towards the end of the project
            /*
            // Arrange
            IEnumerable<FormEntity> dummyForms = new List<FormEntity>
            {
                new() {FormId = 1, ServiceType = ServiceType.Calligraphy, Comments = "Comments 1"},
                new() {FormId = 2, ServiceType = ServiceType.Engraving, Comments = "Comments 2"}
            };
            
            //mock Pagination Filter
            var filter = new PaginationFilter
            {
                PageNumber = 1,
                PageSize = 2
            };
            
            var totalRecords = dummyForms.Count();

            // Act
            _mockFormRepo.Setup(x => x.GetAll(filter, out totalRecords)).Returns(dummyForms);
            var result = _formService.GetAll(filter, "");
            
            //mock Pagination Helper
            _mockPaginationHelper.Setup(x => x.CreatePagedResponse(filter, totalRecords)).Returns(new PagedResponse<IEnumerable<FormEntity>>(dummyForms, filter.PageNumber, filter.PageSize));


            //create PagedResponse object
            var pagedResponse = new PagedResponse<IEnumerable<FormEntity>>(dummyForms, filter.PageNumber, filter.PageSize);

            // Assert
            Assert.Equal(new OkObjectResult(pagedResponse), result);
            */

        }
    }
}
