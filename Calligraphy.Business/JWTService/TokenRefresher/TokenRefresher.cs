using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Calligraphy.Business.AuthenticationService;
using Calligraphy.Business.JWTService.JWTTokenHandler;
using Calligraphy.Data.Models.AuthenticationModels.JWT;
using Calligraphy.Data.Models.AuthenticationModels.Response;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Calligraphy.Business.JWTService.TokenRefresher
{
    public class TokenRefresher : ITokenRefresher
    {
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;
        private readonly IJwtTokenHandler _jwtTokenHandler;

        public TokenRefresher(IAuthService authService, IJwtTokenHandler jwtTokenHandler, IConfiguration configuration)
        {
            _authService = authService;
            _jwtTokenHandler = jwtTokenHandler;
            _configuration = configuration;
        }


        public AuthenticationResponse Refresh(RefreshCred refreshCred)
        {
            bool isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";
            var tokenHandler = new JwtSecurityTokenHandler();
            string validIssuer;
            string validAudience;

            if(isDevelopment)
            {
                validIssuer = "https://localhost:5001";
                validAudience = "https://localhost:5001";
            }
            else
            {
                validIssuer = _configuration["Jwt:Issuer"];
                validAudience = _configuration["Jwt:Audience"];
            }

            var principal = tokenHandler.ValidateToken(refreshCred.JwtToken, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"])),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = validIssuer,
                ValidAudience = validAudience
            }, out var securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            var user = principal.Identity?.Name;

            if (refreshCred.RefreshToken != _authService.GetRefreshToken(user))
                throw new SecurityTokenException("Invalid token");

            return _jwtTokenHandler.Authenticate(user, principal.Claims.ToArray());
        }
    }
}