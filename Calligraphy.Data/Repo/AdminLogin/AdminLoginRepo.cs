using System.Linq;
using Calligraphy.Data.Config;
using Calligraphy.Data.Models;
using Microsoft.Extensions.Logging;

namespace Calligraphy.Data.Repo.AdminLogin
{
    public class AdminLoginRepo  : IAdminLoginRepo
    {
        private readonly CalligraphyContext _context;
        private readonly ILogger<AdminLoginRepo> _logger;

        public AdminLoginRepo(CalligraphyContext context, ILogger<AdminLoginRepo> logger)
        {
            _context = context;
            _logger = logger;
        }

        public bool Login(string username, string password)
        {
            var admin = _context.Admins.FirstOrDefault(a => a.UserName == username && a.Password == password);
            return admin != null;
        }
    }
}