using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calligraphy.Business.Form;
using Calligraphy.Data.Models;
using Calligraphy.Data.Repo;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Calligraphy.Tests.FormTests
{
    [TestClass]
    public class FormServiceTests
    {
        private readonly Mock<IFormRepo> _mockFormRepo;
        private readonly FormService _formService;

        public FormServiceTests()
        {
            _mockFormRepo = new Mock<IFormRepo>();
            _formService = new FormService(_mockFormRepo.Object);
        }

        [TestMethod]
        // test get all
        public void GetAllForms()
        {
            // Arrange
            var forms = new List<FormEntity>
            {
                new FormEntity {FormId = 1, ServiceType = "Service Type 1", Comments = "Comments 1"},
                new FormEntity {FormId = 2, ServiceType = "Service Type 2", Comments = "Comments 2"},
                new FormEntity {FormId = 3, ServiceType = "Service Type 3", Comments = "Comments 3"}
            };
            
            // Act
            _mockFormRepo.Setup(x => x.GetAll()).Returns(forms);
            var result = _formService.GetAll();
            
            // Assert
            Assert.AreEqual(3, result.Count());
            
        }
        
        [TestMethod]
        // Post
        public void CreateForm()
        {
            // Arrange
            var form = new FormEntity {FormId = 1, ServiceType = "Service Type 1", Comments = "Comments 1"};
            
            // Act
            _mockFormRepo.Setup(x => x.Create(form));
            _formService.Create(form);
            
            // Assert
            _mockFormRepo.Verify(x => x.Create(form), Times.Once);
        }
    }
}
