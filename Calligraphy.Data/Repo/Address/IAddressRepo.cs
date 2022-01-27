using Calligraphy.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Calligraphy.Data.Models.AuthenticationModels;

namespace Calligraphy.Data.Repo.Address
{
    public interface IAddressRepo
    {
        IEnumerable<AddressEntity> GetAll();

        bool Create(AddressEntity address);

    }
}