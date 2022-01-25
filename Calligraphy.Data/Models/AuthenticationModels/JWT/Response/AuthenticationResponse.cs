namespace Calligraphy.Data.Models.AuthenticationModels.Response
{
    public class AuthenticationResponse
    {
        public  string JwtToken { get; set; }
        
        public  string RefreshToken { get; set; }
    }
}