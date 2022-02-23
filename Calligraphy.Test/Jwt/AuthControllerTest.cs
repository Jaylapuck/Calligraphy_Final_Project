using Calligraphy.Business.AuthenticationService;
using Calligraphy.Business.JWTService.TokenRefresher;
using Calligraphy.Controllers;
using Calligraphy.Data.Models.AuthenticationModels.JWT;
using Calligraphy.Data.Models.AuthenticationModels.JWT.JWT;
using Calligraphy.Data.Models.AuthenticationModels.Response;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Calligraphy.Test.Jwt
{
    public class AuthControllerTest
    {
        private readonly AdminController _authController;
        private readonly Mock<IAuthService> _authService;
        private readonly Mock<ITokenRefresher> _tokenRefresher;

        public AuthControllerTest()
        {
            _authService = new Mock<IAuthService>();
            _tokenRefresher = new Mock<ITokenRefresher>();
            _authController = new AdminController(_authService.Object, _tokenRefresher.Object);
        }

        //TC9-TC1
        [Fact]
        public void Login_ShouldReturnOkResponseWithAuthenticationResponse()
        {
            // Arrange
            var loginRequest = new AdminEntity
            {
                Id = 1,
                UserName = "test",
                Password = "test"
            };

            var expectedResponse = new AuthenticationResponse
            {
                JwtToken = "test",
                RefreshToken = "test"
            };

            _authService.Setup(x => x.Login(loginRequest)).Returns(expectedResponse);

            // Act
            var response = _authController.Login(loginRequest);

            // Assert
            Assert.IsType<OkObjectResult>(response);
            var result = response as OkObjectResult;
            Assert.Equal(expectedResponse, result.Value);
        }

        //TC9-TC2
        [Fact]
        public void Login_ShouldReturnUnauthorizedResponse()
        {
            // Arrange
            var loginRequest = new AdminEntity
            {
                Id = 1,
                UserName = "test",
                Password = "test"
            };

            var expectedResponse = new AuthenticationResponse
            {
                JwtToken = null,
                RefreshToken = null
            };

            _authService.Setup(x => x.Login(loginRequest)).Returns(expectedResponse);

            // Act
            var response = _authController.Login(loginRequest);

            // Assert
            Assert.IsType<UnauthorizedObjectResult>(response);
        }

        //TC9-TC3
        [Fact]
        public void RefreshToken_ShouldReturnOkResponseWithAuthenticationResponse()
        {
            // Arrange
            var refreshTokenRequest = new RefreshCred
            {
                JwtToken = "test",
                RefreshToken = "test"
            };

            var expectedResponse = new AuthenticationResponse
            {
                JwtToken = "test",
                RefreshToken = "test"
            };

            var tokenOnly = new TokenOnly()
            {
                JwtToken = "test"
            };

        _tokenRefresher.Setup(x => x.Refresh(refreshTokenRequest)).Returns(expectedResponse);

            // Act
            var response = _authController.Refresh(tokenOnly);

            // Assert
            Assert.IsType<OkObjectResult>(response);
            var result = response as OkObjectResult;
            Assert.Equal(expectedResponse, result.Value);
        }

        //TC9-TC4
        [Fact]
        public void RefreshToken_ShouldReturnUnauthorizedResponse()
        {
            // Arrange
            var refreshTokenRequest = new RefreshCred
            {
                JwtToken = "test",
                RefreshToken = "test"
            };

            var expectedResponse = new AuthenticationResponse
            {
                JwtToken = null,
                RefreshToken = null
            };
            
            var tokenOnly = new TokenOnly()
            {
                JwtToken = "test"
            };

            _tokenRefresher.Setup(x => x.Refresh(refreshTokenRequest)).Returns(expectedResponse);

            // Act
            var response = _authController.Refresh(tokenOnly);

            // Assert
            Assert.IsType<UnauthorizedObjectResult>(response);
        }
    }
}