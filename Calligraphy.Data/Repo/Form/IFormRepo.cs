using Calligraphy.Data.Models;
using Calligraphy.Data.Pagination;

namespace Calligraphy.Data.Repo.Form
{
    public interface IFormRepo
    {
        PagedList<FormEntity> GetAll(FormParameters formParameters);

        bool Create(FormEntity form);
    }
}