using System;
using System.Security.Cryptography;

namespace Calligraphy.Business.JWTService.RefreshTokenGenerator
{
    public class RefreshTokenGenerator : IRefreshTokenGenerator
    {
        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];

            using var randomNumberGenerator = RandomNumberGenerator.Create();

            randomNumberGenerator.GetBytes(randomNumber);

            return Convert.ToBase64String(randomNumber);
        }
    }
}