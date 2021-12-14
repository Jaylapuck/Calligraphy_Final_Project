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
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Calligraphy.Test.Form
{
    public class FormServiceTests
    {
        private readonly Mock<IFormRepo> _mockFormRepo;
        private readonly FormService _formService;

        public FormServiceTests()
        {
            _mockFormRepo = new Mock<IFormRepo>();
            _formService = new FormService(_mockFormRepo.Object);
        }

        [Fact]
        // TS2-TC5
        public void GetAllForms()
        {
            // Arrange

            AddressEntity address1 = new AddressEntity { AddressId = 1, Street = "1000 Rue ThisSide", Postal = "J1Y1P1", City = "Montreal", Country = "Canada" };
            AddressEntity address2 = new AddressEntity { AddressId = 2, Street = "2000 Rue ThatSide", Postal = "J2Y2P2", City = "Ottawa", Country = "Canada" };
            AddressEntity address3 = new AddressEntity { AddressId = 3, Street = "3000 Rue OtherSide", Postal = "J3Y3P3", City = "Toronto", Country = "Canada" };

            var forms = new List<FormEntity>
            {
                new FormEntity {FormId = 1, FirstName="James", LastName="Albe", Address=address1, ServiceType = ServiceType.Calligraphy, Comments = "Comments 1"},
                new FormEntity {FormId = 2, FirstName="John", LastName="Doe", Address=address2, ServiceType = ServiceType.Engraving, Comments = "Comments 2"},
                new FormEntity {FormId = 3, FirstName="Jane", LastName="Smith", Address=address3, ServiceType = ServiceType.Event, Comments = "Comments 3"}
            };
            
            // Act
            _mockFormRepo.Setup(x => x.GetAll()).Returns(forms);
            var result = _formService.GetAll();
            
            // Assert
            Assert.Equal(3, result.Count());
            
        }
        
        [Fact]
        // TS1-TC6
        public void CreateForm()
        {
            // Arrange
            var service = ServiceType.Calligraphy;
            var address = new AddressEntity { AddressId = 1, Street = "2705 Rue Marquise", Postal = "J4Y1P1", City = "Montreal", Country = "Canada" };
            var form = new FormEntity {FormId = 1, FirstName="Jordan", LastName="Albayrak", Address=address, ServiceType = service, Comments = "Comments 1"};
            
            // Act
            _mockFormRepo.Setup(x => x.Create(form));
            _formService.Create(form);
            
            // Assert
            _mockFormRepo.Verify(x => x.Create(form), Times.Once);
        }
    }
}
