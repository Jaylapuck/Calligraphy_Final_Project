using Calligraphy.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Calligraphy.Data.Repo.Service
{
    public interface IServiceRepo
    {
        IEnumerable<ServiceEntity> GetAll();
    }
}
