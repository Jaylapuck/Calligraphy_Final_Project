using Calligraphy.Data.Models.AuthenticationModels.JWT;
using Calligraphy.Data.Models.AuthenticationModels.Response;

namespace Calligraphy.Business.JWTService.TokenRefresher
{
    public interface ITokenRefresher
    {
        AuthenticationResponse Refresh(RefreshCred refreshCred);
    }
}