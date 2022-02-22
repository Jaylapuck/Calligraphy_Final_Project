using System;
using Calligraphy.Data.Models.AuthenticationModels.JWT.JWT;

namespace Calligraphy.Data.Repo.AdminLogin
{
    public interface IAdminLoginRepo
    {
        bool Login(string username, string password);

        bool AddRefreshTokenToUser(string username, string refreshToken, DateTime refreshTokenExpiry);
        RefreshCredWithExpiration GetRefreshToken(string userName);
    }
}