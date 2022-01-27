namespace Calligraphy.Data.Models.AuthenticationModels.JWT
{
    public class RefreshCred
    {
        public string JwtToken { get; set; }
        
        public string RefreshToken { get; set; }
    }
}