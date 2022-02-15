namespace Calligraphy.Data.Repo.AdminLogin
{
    public interface IAdminLoginRepo
    {
        bool Login(string username, string password);

        bool AddRefreshTokenToUser(string username, string refreshToken);
        string GetRefreshToken(string userName);
    }
}