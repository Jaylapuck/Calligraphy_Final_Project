using System;
using Calligraphy.Business.AuthenticationService;
using Calligraphy.Business.JWTService.TokenRefresher;
using Calligraphy.Controllers;
using Calligraphy.Data.Models.AuthenticationModels.JWT;
using Calligraphy.Data.Models.AuthenticationModels.Response;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Calligraphy.Test.Jwt
{
    public class AuthControllerTest
    {
        private readonly Mock<IAuthService> _authService;
        private readonly Mock<ITokenRefresher> _tokenRefresher;
        private readonly AdminController _authController;
        
        public AuthControllerTest()
        {
            _authService = new Mock<IAuthService>();
            _tokenRefresher = new Mock<ITokenRefresher>();
            _authController = new AdminController(_authService.Object, _tokenRefresher.Object);
        }

        [Fact]
        public void Login_ShouldReturnOkResponseWithAuthenticationResponse()
        {
            // Arrange
            var loginRequest = new AdminEntity()
            {
                Id = 1,
                UserName = "test",
                Password = "test"
            };
            
            var expectedResponse = new AuthenticationResponse()
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

        [Fact]
        public void Login_ShouldReturnUnauthorizedResponse()
        {
            // Arrange
            var loginRequest = new AdminEntity()
            {
                Id = 1,
                UserName = "test",
                Password = "test"
            };
            
            var expectedResponse = new AuthenticationResponse()
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
        
        [Fact]
        public void RefreshToken_ShouldReturnOkResponseWithAuthenticationResponse()
        {
            // Arrange
            var refreshTokenRequest = new RefreshCred()
            {
                JwtToken = "test",
                RefreshToken = "test"
            };
            
            var expectedResponse = new AuthenticationResponse()
            {
                JwtToken = "test",
                RefreshToken = "test"
            };
            
            _tokenRefresher.Setup(x => x.Refresh(refreshTokenRequest)).Returns(expectedResponse);
            
            // Act
            var response = _authController.Refresh(refreshTokenRequest);
            
            // Assert
            Assert.IsType<OkObjectResult>(response);
            var result = response as OkObjectResult;
            Assert.Equal(expectedResponse, result.Value);
        }
        
        [Fact]
        public void RefreshToken_ShouldReturnUnauthorizedResponse()
        {
            // Arrange
            var refreshTokenRequest = new RefreshCred()
            {
                JwtToken = "test",
                RefreshToken = "test"
            };
            
            var expectedResponse = new AuthenticationResponse()
            {
                JwtToken = null,
                RefreshToken = null
            };
            
            _tokenRefresher.Setup(x => x.Refresh(refreshTokenRequest)).Returns(expectedResponse);
            
            // Act
            var response = _authController.Refresh(refreshTokenRequest);
            
            // Assert
            Assert.IsType<UnauthorizedObjectResult>(response);
        }
    }
}