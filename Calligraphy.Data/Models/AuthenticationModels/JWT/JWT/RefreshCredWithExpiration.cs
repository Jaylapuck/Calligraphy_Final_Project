using System;

namespace Calligraphy.Data.Models.AuthenticationModels.JWT.JWT
{
    public class RefreshCredWithExpiration
    {
        public string RefreshToken { get; set; }
        public DateTime Expiration { get; set; }
    }
}