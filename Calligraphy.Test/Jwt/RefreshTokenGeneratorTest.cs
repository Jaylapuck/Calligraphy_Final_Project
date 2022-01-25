using System;
using Calligraphy.Business.JWTService.RefreshTokenGenerator;
using Xunit;

namespace Calligraphy.Test.Jwt
{
    public class RefreshTokenGeneratorTest
    {
        private readonly RefreshTokenGenerator _refreshTokenGenerator;

        public RefreshTokenGeneratorTest()
        {
            _refreshTokenGenerator = new RefreshTokenGenerator();
        }

        [Fact]
        public void GenerateRefreshToken_ShouldReturnToken()
        {
            var token = _refreshTokenGenerator.GenerateRefreshToken();

            Assert.NotNull(token);
        }
    }
}