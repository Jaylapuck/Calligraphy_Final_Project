using System.Collections.Generic;
using Calligraphy.Data.Models;

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