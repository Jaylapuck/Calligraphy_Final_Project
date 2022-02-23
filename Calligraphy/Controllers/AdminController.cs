using System.Net;
using Calligraphy.Business.AuthenticationService;
using Calligraphy.Business.JWTService.TokenRefresher;
using Calligraphy.Data.Models.AuthenticationModels.JWT;
using Calligraphy.Data.Models.AuthenticationModels.JWT.JWT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Calligraphy.Controllers
{
    [Route("api/admin")]
    [Authorize]
    public class AdminController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ITokenRefresher _tokenRefresher;

        public AdminController(IAuthService authService, ITokenRefresher tokenRefresher)
        {
            _authService = authService;
            _tokenRefresher = tokenRefresher;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] AdminEntity admin)
        {
            var token = _authService.Login(admin);

            if (token.JwtToken == null || token.RefreshToken == null)
                return Unauthorized(new
                {
                    HttpCode = HttpStatusCode.Unauthorized,
                    message = "Invalid username or password"
                });
            
            HttpContext.Response.Cookies.Append("RefreshToken", token.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true
            });
            
            //only return the token
            return Ok(new
            {
                token.JwtToken
            });
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("refresh")]
        public IActionResult Refresh([FromBody] TokenOnly tokenOnly)
        {
            var refreshToken = HttpContext.Request.Cookies["RefreshToken"];

            var refreshCred = new RefreshCred()
            {
                JwtToken = tokenOnly.JwtToken,
                RefreshToken = refreshToken
            };
            
            var token = _tokenRefresher.Refresh(refreshCred);

            if (token.JwtToken == null || token.RefreshToken == null)
                return Unauthorized(new
                {
                    HttpCode = HttpStatusCode.Unauthorized,
                    message = "Invalid username or password"
                });
            
            //make cookie http only
            HttpContext.Response.Cookies.Append("RefreshToken", token.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true
            });
            
            return Ok(token);
        }

        [HttpGet]
        [Route("verify")]
        public IActionResult CheckIfTokenIsValid()
        {
            return Ok(new
            {
                HttpStatusCode = HttpStatusCode.OK,
                message = "Token is valid"
            });
        }
    }
}