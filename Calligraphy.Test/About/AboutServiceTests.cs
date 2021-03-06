using Calligraphy.Business.About;
using Calligraphy.Data.Models;
using Calligraphy.Data.Repo.About;
using Moq;
using Xunit;

namespace Calligraphy.Test.About
{
    public class AboutServiceTests
    {
        private readonly AboutService _aboutService;
        private readonly Mock<IAboutRepo> _mockAboutRepo;

        public string name1 = "name1",
            email1 = "email1",
            phone1 = "phone1",
            proffesion1 = "prof1",
            description1 = "desc1",
            language1 = "lang1",
            country1 = "count1",
            experience1 = "exp1",
            mission1 = "miss1";


        public AboutServiceTests()
        {
            _mockAboutRepo = new Mock<IAboutRepo>();
            _aboutService = new AboutService(_mockAboutRepo.Object);
        }

        [Fact]
        // TC8-TS1
        public void Get_DoesNotReturnNull()
        {
            // Arrange
            var about = new AboutEntity
            {
                AboutId = 1,
                Name = name1,
                Email = email1,
                Phone = phone1,
                Profession = proffesion1,
                Description = description1,
                Language = language1,
                Country = country1,
                Experience = experience1,
                Mission = mission1
            };

            // Act
            _mockAboutRepo.Setup(x => x.Get()).Returns(about);
            var result = _aboutService.Get();


            // Assert 
            Assert.NotNull(result);
        }
    }
}