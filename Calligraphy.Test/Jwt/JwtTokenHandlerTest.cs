using System;
using System.Security.Claims;
using Calligraphy.Business.JWTService.JWTTokenHandler;
using Calligraphy.Business.JWTService.RefreshTokenGenerator;
using Calligraphy.Data.Models.AuthenticationModels.Response;
using Calligraphy.Data.Repo.AdminLogin;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace Calligraphy.Test.Jwt
{
    public class JwtTokenHandlerTest
    {
        private readonly JwtTokenHandler _jwtTokenHandler;
        private readonly Mock<IConfiguration> _configurationMock;

        public JwtTokenHandlerTest()
        {
            var refreshTokenGeneratorMock = new Mock<IRefreshTokenGenerator>();
            _configurationMock = new Mock<IConfiguration>();
            var adminLoginRepoMock = new Mock<IAdminLoginRepo>();
            _jwtTokenHandler = new JwtTokenHandler(refreshTokenGeneratorMock.Object, adminLoginRepoMock.Object, _configurationMock.Object);
        }
        
        [Fact]
        public void AuthenticateWithUsername_Should_Return_AuthenticationResponse()
        {
            // Arrange
            var username = "username";
            _configurationMock.Setup(x => x["Jwt:Secret"]).Returns("mysupersecretkeythatbelongstome");
            
            
            // Act
            var result = _jwtTokenHandler.Authenticate(username);
            
            // Assert
            Assert.NotNull(result);
            Assert.IsType<AuthenticationResponse>(result);
        }

        [Fact]
        public void AuthenticateWithUsernameAndListOfClaim_Should_Return_AuthenticatedResponse()
        {
            // Arrange
            var username = "username";
            var claims = new Claim[]
            {
                new(ClaimTypes.Name, username)
            };
            _configurationMock.Setup(x => x["Jwt:Secret"]).Returns("mysupersecretkeythatbelongstome");
            
            // Act
            var result = _jwtTokenHandler.Authenticate(username, claims);
            
            // Assert
            Assert.NotNull(result);
            Assert.IsType<AuthenticationResponse>(result);
        }
    }
}