using Calligraphy.Business.AuthenticationService;
using Calligraphy.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace Calligraphy.Controllers
{
    public class AdminController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AdminController(IAuthService authService)
        {
            _authService = authService;
        }


        [HttpPost]
        [Route("api/admin/login")]
        public IActionResult Login([FromBody] AdminEntity admin)
        {
            var result = _authService.Login(admin);
            return result;
        }
    }
}