using System.Collections.Generic;
using Calligraphy.Data.Models;

namespace Calligraphy.Data.Repo
{
    public interface IFormRepo
    {
        IEnumerable<FormEntity> GetAll();

        FormEntity Create(FormEntity form);

    }
}