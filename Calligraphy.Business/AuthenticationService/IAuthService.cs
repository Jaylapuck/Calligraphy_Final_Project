using Calligraphy.Data.Models.AuthenticationModels.JWT;
using Calligraphy.Data.Models.AuthenticationModels.JWT.JWT;
using Calligraphy.Data.Models.AuthenticationModels.Response;

namespace Calligraphy.Business.AuthenticationService
{
    public interface IAuthService
    {
        AuthenticationResponse Login(AdminEntity admin);
        RefreshCredWithExpiration GetRefreshToken(string userName);
    }
}