using Calligraphy.Business.JWTService.JWTTokenHandler;
using Calligraphy.Data.Models.AuthenticationModels.JWT;
using Calligraphy.Data.Models.AuthenticationModels.JWT.JWT;
using Calligraphy.Data.Models.AuthenticationModels.Response;
using Calligraphy.Data.Repo.AdminLogin;

namespace Calligraphy.Business.AuthenticationService
{
    public class AuthService : IAuthService
    {
        private readonly IAdminLoginRepo _authRepo;
        private readonly IJwtTokenHandler _jwtTokenHandler;

        public AuthService(IAdminLoginRepo authRepo, IJwtTokenHandler jwtTokenHandler)
        {
            _authRepo = authRepo;
            _jwtTokenHandler = jwtTokenHandler;
        }

        public AuthenticationResponse Login(AdminEntity entity)
        {
            var isLogin = _authRepo.Login(entity.UserName, entity.Password);

            if (!isLogin)
                return new AuthenticationResponse
                {
                    JwtToken = null,
                    RefreshToken = null
                };

            var token = _jwtTokenHandler.Authenticate(entity.UserName);

            return token;
        }

        public RefreshCredWithExpiration GetRefreshToken(string userName)
        {
            return _authRepo.GetRefreshToken(userName);
        }
    }
}