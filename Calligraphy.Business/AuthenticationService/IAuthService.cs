using Calligraphy.Data.Models;
using Calligraphy.Data.Models.AuthenticationModels;
using Calligraphy.Data.Models.AuthenticationModels.JWT;
using Calligraphy.Data.Models.AuthenticationModels.Response;
using Microsoft.AspNetCore.Mvc;

namespace Calligraphy.Business.AuthenticationService
{
    public interface IAuthService
    {
        AuthenticationResponse Login(AdminEntity admin);
        string GetRefreshToken(string userName);
    }
}