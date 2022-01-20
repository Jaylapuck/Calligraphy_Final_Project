using Calligraphy.Data.Models;

namespace Calligraphy.Data.Repo.AdminLogin
{
    public interface IAdminLoginRepo
    { 
        bool Login(string username, string password);
    }
}