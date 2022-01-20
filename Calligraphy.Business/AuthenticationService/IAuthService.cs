using Calligraphy.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace Calligraphy.Business.AuthenticationService
{
    public interface IAuthService
    {
        IActionResult Login(AdminEntity admin);
    }
}