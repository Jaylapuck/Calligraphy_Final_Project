using System.Collections.Generic;
using Calligraphy.Data.Models;

namespace Calligraphy.Data.Repo.Form
{
    public interface IFormRepo
    {
        IEnumerable<FormEntity> GetAll();

        IEnumerable<FormEntity> GetAllPageable(int pageNumber, int pageSize);

        bool Create(FormEntity form);
    }

}