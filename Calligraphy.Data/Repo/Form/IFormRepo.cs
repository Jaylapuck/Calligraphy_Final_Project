using System.Collections.Generic;
using System.Threading.Tasks;
using Calligraphy.Data.Filters;
using Calligraphy.Data.Models;

namespace Calligraphy.Data.Repo.Form
{
    public interface IFormRepo
    {
        IEnumerable<FormEntity> GetAll(PaginationFilter validFilter, out int totalRecords);

        bool Create(FormEntity form);
    }

}