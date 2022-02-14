using System.Collections.Generic;
using Calligraphy.Data.Models;

namespace Calligraphy.Data.Repo.Service
{
    public interface IServiceRepo
    {
        IEnumerable<ServiceEntity> GetAll();
    }
}