using System.Collections.Generic;
using System.Security.Claims;
using Calligraphy.Data.Models.AuthenticationModels.Response;

namespace Calligraphy.Business.JWTService.JWTTokenHandler
{
    public interface IJwtTokenHandler
    {
        AuthenticationResponse Authenticate(string username);

        AuthenticationResponse Authenticate(string username, IEnumerable<Claim> claims);
    }
}