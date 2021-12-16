using Calligraphy.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calligraphy.Business.Address
{
    public interface IAddressService
    {
        // GET ALLs
        IEnumerable<AddressEntity> GetAll();

        // POST
        bool Create(AddressEntity address);
    }
}
