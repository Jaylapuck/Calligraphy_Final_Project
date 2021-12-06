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
        // GET ALL
        IEnumerable<FormEntity> GetAll();

        // POST
        FormEntity Create(FormEntity form);
    }
}
