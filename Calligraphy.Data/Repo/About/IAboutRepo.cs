using Calligraphy.Data.Models;

namespace Calligraphy.Data.Repo.About
{
    public interface IAboutRepo
    {
        AboutEntity Get();
        AboutEntity Update(AboutEntity about);
    }
}