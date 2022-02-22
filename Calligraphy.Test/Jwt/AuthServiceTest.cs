using System;
using Calligraphy.Business.AuthenticationService;
using Calligraphy.Business.JWTService.JWTTokenHandler;
using Calligraphy.Data.Models.AuthenticationModels.JWT;
using Calligraphy.Data.Models.AuthenticationModels.JWT.JWT;
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
        private readonly AuthService _authService;

        public AuthServiceTest()
        {
            _adminLoginRepo = new Mock<IAdminLoginRepo>();
            _jwtTokenHandler = new Mock<IJwtTokenHandler>();
            _authService = new AuthService(_adminLoginRepo.Object, _jwtTokenHandler.Object);
        }

        //TC9-TS1
        [Fact]
        public void Login_ShouldReturnAuthenticationResponse()
        {
            // Arrange
            var adminLogin = new AdminEntity
            {
                Id = 1,
                UserName = "admin",
                Password = "admin"
            };

            var authenticateResponse = new AuthenticationResponse
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

        //TC9-TS2
        [Fact]
        public void Login_ShouldReturnAuthenticationResponseBecauseOfLoginDidNotWork()
        {
            // Arrange
            var adminLogin = new AdminEntity
            {
                Id = 1,
                UserName = "admin",
                Password = "admin"
            };

            var authenticateResponse = new AuthenticationResponse
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

        //TC9-TS3
        [Fact]
        public void GetRefreshToken()
        {
            // Arrange
            const string username = "admin";
            
            var refreshCredWithExpiration = new RefreshCredWithExpiration
            {
                RefreshToken = "refreshToken",
                Expiration = DateTime.Now.AddDays(1)
            };

            _adminLoginRepo.Setup(x => x.GetRefreshToken(username)).Returns(refreshCredWithExpiration);

            // Act
            var result = _authService.GetRefreshToken(username);

            // Assert
            Assert.Equal(refreshCredWithExpiration.RefreshToken, result.RefreshToken);
            Assert.Equal(refreshCredWithExpiration.Expiration, result.Expiration);
            Assert.IsType<RefreshCredWithExpiration>(result);
        }
    }
}