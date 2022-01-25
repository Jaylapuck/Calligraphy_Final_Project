using System.Linq;
using Calligraphy.Data.Config;
using Calligraphy.Data.Models;
using Microsoft.Extensions.Logging;

namespace Calligraphy.Data.Repo.AdminLogin
{
    public class AdminLoginRepo  : IAdminLoginRepo
    {
        private readonly CalligraphyContext _context;

        public AdminLoginRepo(CalligraphyContext context)
        {
            _context = context;
        }

        public bool Login(string username, string password)
        {
            var admin = _context.Admins.FirstOrDefault(a => a.UserName == username && a.Password == password);
            return admin != null;
        }
        
        public bool AddRefreshTokenToUser(string username, string refreshToken)
        {
            var admin = _context.Admins.FirstOrDefault(a => a.UserName == username);
            
            if (admin == null)
            {
                return false;
            }
            
            admin.RefreshToken = refreshToken;
            _context.SaveChanges();
            return true;
        }

        public string GetRefreshToken(string userName)
        {
            var admin = _context.Admins.FirstOrDefault(a => a.UserName == userName);
            return admin?.RefreshToken;
        }
        
    }
}