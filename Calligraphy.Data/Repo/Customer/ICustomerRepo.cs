using System.Collections.Generic;
using Calligraphy.Data.Models;

namespace Calligraphy.Data.Repo.Customer
{
    public interface ICustomerRepo
    {
        IEnumerable<CustomerEntity> GetAll();

        bool Create(CustomerEntity customer);
    }
}