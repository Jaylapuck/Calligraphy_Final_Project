using Calligraphy.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Calligraphy.Data.Repo.Customer
{
    public interface ICustomerRepo
    {
        IEnumerable<CustomerEntity> GetAll();

        bool Create(CustomerEntity customer);

    }
}