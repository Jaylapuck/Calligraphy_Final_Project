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
        private readonly IConfiguration _configuration;
        private readonly IRefreshTokenGenerator _refreshTokenGenerator;
        private readonly IAdminLoginRepo _adminLoginRepo;
        
        public JwtTokenHandler(IRefreshTokenGenerator refreshTokenGenerator, IAdminLoginRepo adminLoginRepo, IConfiguration configuration)
        {
            _refreshTokenGenerator = refreshTokenGenerator;
            _adminLoginRepo = adminLoginRepo;
            _configuration = configuration;
        }

        //Refresh Token Authentication
        public AuthenticationResponse Authenticate(string username, IEnumerable<Claim> claims)
        {
            var jwtSecurityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                audience: _configuration["Jwt:Audience"],
                issuer: _configuration["Jwt:Issuer"],
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"])), SecurityAlgorithms.HmacSha256)
            );

            var token =  new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
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
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                Audience = _configuration["Jwt:Audience"],
                Issuer = _configuration["Jwt:Issuer"],
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