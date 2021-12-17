using Calligraphy.Data.Models;
using Calligraphy.Data.Repo.Address;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calligraphy.Business.Address
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepo _addressRepo;

        public AddressService(IAddressRepo addressRepo)
        {
            _addressRepo = addressRepo;
        }

        public AddressService()
        {
            _addressRepo = new AddressRepo();
        }

        public IEnumerable<AddressEntity> GetAll()
        {
            return _addressRepo.GetAll();
        }

        //Add new form
        public bool Create(AddressEntity address)
        {
            return _addressRepo.Create(address);
        }

    }
}