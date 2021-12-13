using Calligraphy.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calligraphy.Business.Form
{
    public interface IFormService
    {
        // GET ALLs
        IEnumerable<FormEntity> GetAll();

        // POST
        bool Create(FormEntity form);
    }
}
