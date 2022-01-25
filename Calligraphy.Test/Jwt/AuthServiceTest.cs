using Calligraphy.Business.AuthenticationService;
using Calligraphy.Business.JWTService.JWTTokenHandler;
using Calligraphy.Data.Models.AuthenticationModels.JWT;
using Calligraphy.Data.Models.AuthenticationModels.Response;
using Calligraphy.Data.Repo.AdminLogin;
using Moq;
using Xunit;

namespace Calligraphy.Test.Jwt
{
    public class AuthServiceTest
    {
        private readonly Mock<IAdminLoginRepo> _adminLoginRepo;
        private readonly Mock<IJwtTokenHandler> _jwtTokenHandler;
        private AuthService _authService;

        public AuthServiceTest()
        {
            _adminLoginRepo = new Mock<IAdminLoginRepo>();
            _jwtTokenHandler = new Mock<IJwtTokenHandler>();
            _authService = new AuthService(_adminLoginRepo.Object, _jwtTokenHandler.Object);
        }

        [Fact]
        public void Login_ShouldReturnAuthenticationResponse()
        {
            // Arrange
            var adminLogin = new AdminEntity()
            {
                Id = 1,
                UserName = "admin",
                Password = "admin"
            };

            var authenticateResponse = new AuthenticationResponse()
            {
                JwtToken = "token",
                RefreshToken = "refreshToken"
            };

            _adminLoginRepo.Setup(x => x.Login(adminLogin.UserName, adminLogin.Password)).Returns(true);
            _jwtTokenHandler.Setup(x => x.Authenticate(adminLogin.UserName)).Returns(authenticateResponse);

            // Act
            var result = _authService.Login(adminLogin);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(authenticateResponse.JwtToken, result.JwtToken);
            Assert.Equal(authenticateResponse.RefreshToken, result.RefreshToken);
            Assert.IsType<AuthenticationResponse>(result);
        }

        [Fact]
        public void Login_ShouldReturnAuthenticationResponseBecauseOfLoginDidNotWork()
        {
            // Arrange
            var adminLogin = new AdminEntity()
            {
                Id = 1,
                UserName = "admin",
                Password = "admin"
            };

            var authenticateResponse = new AuthenticationResponse()
            {
                JwtToken = null,
                RefreshToken = null
            };


            _adminLoginRepo.Setup(x => x.Login(adminLogin.UserName, adminLogin.Password)).Returns(false);

            // Act
            var result = _authService.Login(adminLogin);

            // Assert
            Assert.Equal(authenticateResponse.JwtToken, result.JwtToken);
            Assert.Equal(authenticateResponse.RefreshToken, result.RefreshToken);
            Assert.IsType<AuthenticationResponse>(result);
        }

        [Fact]
        public void GetRefreshToken()
        {
            // Arrange
            var username = "admin";
            
            _adminLoginRepo.Setup(x => x.GetRefreshToken(username)).Returns("refreshToken");
            
            // Act
            var result = _authService.GetRefreshToken(username);
            
            // Assert
            Assert.NotNull(result);
            Assert.IsType<string>(result);
        }
    }
}