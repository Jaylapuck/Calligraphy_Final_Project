using Calligraphy.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Calligraphy.Data.Repo.Address
{
    public interface IAddressRepo
    {
        IEnumerable<AddressEntity> GetAll();

        bool Create(AddressEntity address);

    }
}