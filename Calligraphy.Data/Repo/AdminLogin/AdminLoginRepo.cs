using System;
using System.Linq;
using Calligraphy.Data.Config;
using Calligraphy.Data.Models.AuthenticationModels.JWT.JWT;

namespace Calligraphy.Data.Repo.AdminLogin
{
    public class AdminLoginRepo : IAdminLoginRepo
    {
        private readonly CalligraphyContext _context;

        public AdminLoginRepo(CalligraphyContext context)
        {
            _context = context;
        }

        public bool Login(string username, string password)
        {
            var admin = _context.Admins.FirstOrDefault(a => a.UserName == username);
            return admin != null && BCrypt.Net.BCrypt.Verify(password, admin.Password);
        }
        
        public bool AddRefreshTokenToUser(string username, string refreshToken, DateTime refreshTokenExpiry)
        {
            var admin = _context.Admins.FirstOrDefault(a => a.UserName == username);
            if (admin == null) return false;
            admin.RefreshToken = refreshToken;
            admin.RefreshTokenExpirationDate = refreshTokenExpiry;
            _context.SaveChanges();
            return true;
        }

        public RefreshCredWithExpiration GetRefreshToken(string userName)
        {
            var admin = _context.Admins.FirstOrDefault(a => a.UserName == userName);
            if (admin == null) return null;
            return new RefreshCredWithExpiration
            {
                RefreshToken = admin.RefreshToken,
                Expiration = admin.RefreshTokenExpirationDate
            };
        }
    }
}