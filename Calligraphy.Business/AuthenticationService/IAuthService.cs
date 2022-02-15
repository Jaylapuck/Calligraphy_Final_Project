using Calligraphy.Data.Models.AuthenticationModels.JWT;
using Calligraphy.Data.Models.AuthenticationModels.Response;

namespace Calligraphy.Business.AuthenticationService
{
    public interface IAuthService
    {
        AuthenticationResponse Login(AdminEntity admin);
        string GetRefreshToken(string userName);
    }
}