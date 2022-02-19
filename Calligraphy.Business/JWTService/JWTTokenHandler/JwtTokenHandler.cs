using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Calligraphy.Business.JWTService.RefreshTokenGenerator;
using Calligraphy.Data.Models.AuthenticationModels.Response;
using Calligraphy.Data.Repo.AdminLogin;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Calligraphy.Business.JWTService.JWTTokenHandler
{
    public class JwtTokenHandler : IJwtTokenHandler
    {
        private readonly IAdminLoginRepo _adminLoginRepo;
        private readonly IConfiguration _configuration;
        private readonly IRefreshTokenGenerator _refreshTokenGenerator;

        public JwtTokenHandler(IRefreshTokenGenerator refreshTokenGenerator, IAdminLoginRepo adminLoginRepo,
            IConfiguration configuration)
        {
            _refreshTokenGenerator = refreshTokenGenerator;
            _adminLoginRepo = adminLoginRepo;
            _configuration = configuration;
        }

        //Refresh Token Authentication
        public AuthenticationResponse Authenticate(string username, IEnumerable<Claim> claims)
        {
            bool isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";
            JwtSecurityToken jwtSecurityToken;

            string validIssuer;
            string validAudience;

            if (isDevelopment)
            {
                validIssuer = "https://localhost:5001";
                validAudience = "https://localhost:5001";
            }
            else
            {
                validIssuer = _configuration["Jwt:Issuer"];
                validAudience = _configuration["Jwt:Audience"];
            }

            jwtSecurityToken = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(1),
                    audience: validAudience,
                    issuer: validIssuer,
                    signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"])),
                        SecurityAlgorithms.HmacSha256)
            );

            var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            var refreshToken = _refreshTokenGenerator.GenerateRefreshToken();

            _adminLoginRepo.AddRefreshTokenToUser(username, refreshToken);

            return new AuthenticationResponse
            {
                JwtToken = token,
                RefreshToken = refreshToken
            };
        }

        //Authorization Token Authentication
        public AuthenticationResponse Authenticate(string username)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"]);
            bool isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";
            SecurityTokenDescriptor tokenDescriptor;

            string validIssuer;
            string validAudience;

            if (isDevelopment)
            {
                validIssuer = "https://localhost:5001";
                validAudience = "https://localhost:5001";
            }
            else
            {
                validIssuer = _configuration["Jwt:Issuer"];
                validAudience = _configuration["Jwt:Audience"];
            }

            tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                    Audience = validAudience,
                    Issuer = validIssuer,
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
                };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var refreshToken = _refreshTokenGenerator.GenerateRefreshToken();

            _adminLoginRepo.AddRefreshTokenToUser(username, refreshToken);

            return new AuthenticationResponse
            {
                JwtToken = tokenHandler.WriteToken(token),
                RefreshToken = refreshToken
            };
        }
    }
}