#nullable enable
using System.Collections.Generic;
using Calligraphy.Data.Models;
using Calligraphy.Data.Pagination;

namespace Calligraphy.Business.Form
{
    public interface IFormService
    {
        // GET ALLs
        PagedList<FormEntity> GetAll(FormParameters formParameters);

        IEnumerable<ServiceEntity> GetAllServices();

        // POST
        bool Create(FormEntity? form);
    }
}