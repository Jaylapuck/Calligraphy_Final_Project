using System.Collections.Generic;
using Calligraphy.Data.Models;

namespace Calligraphy.Data.Repo.Address
{
    public interface IAddressRepo
    {
        IEnumerable<AddressEntity> GetAll();

        bool Create(AddressEntity address);
    }
}