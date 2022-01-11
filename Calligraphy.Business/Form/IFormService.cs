using Calligraphy.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calligraphy.Data.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Calligraphy.Business.Form
{
    public interface IFormService
    {
        // GET ALLs
        IActionResult GetAll(PaginationFilter filter, string? route);
        
        IEnumerable<ServiceEntity> GetAllServices();

        // POST
        bool Create(FormEntity? form);
    }
}
