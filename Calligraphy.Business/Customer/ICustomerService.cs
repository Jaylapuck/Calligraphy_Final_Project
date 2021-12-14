using Calligraphy.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calligraphy.Business.Customer
{
    public interface ICustomerService
    {
        // GET ALLs
        IEnumerable<CustomerEntity> GetAll();

        // POST
        bool Create(CustomerEntity customer);
    }
}
