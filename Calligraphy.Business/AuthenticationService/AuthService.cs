using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Calligraphy.Data.Models;
using Calligraphy.Data.Repo.AdminLogin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Calligraphy.Business.AuthenticationService
{
    public class AuthService : IAuthService
    {
        private readonly IAdminLoginRepo _authRepo;

        public AuthService(IAdminLoginRepo authRepo)
        {
            _authRepo = authRepo;
        }

        public IActionResult Login(AdminEntity model)
        {
            var isLogin = _authRepo.Login(model.UserName, model.Password);

            if (!isLogin) return new UnauthorizedResult();
            var  secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("@YT6*}HnibVtC4?ubl^4ybr1#ekn=<UCN]86T^=yA[8Ivz`ZasJy+GwOr=8avZU"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            
            var tokenOptions = new JwtSecurityToken(
                audience: "https://localhost:5001",
                issuer: "https://localhost:5001",
                claims: new[]
                {
                    new Claim(ClaimTypes.Name, model.UserName),
                },
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: signinCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return new OkObjectResult(new { Token = tokenString });
        }
    }
}