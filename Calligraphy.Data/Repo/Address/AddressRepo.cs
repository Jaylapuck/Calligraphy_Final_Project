using System.Collections.Generic;
using System.Linq;
using Calligraphy.Data.Config;
using Calligraphy.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Calligraphy.Data.Repo.Address
{
    public class AddressRepo : IAddressRepo
    {
        private readonly CalligraphyContext _context;
        public AddressRepo(CalligraphyContext context)
        {
            _context = context;
        }

        public IEnumerable<AddressEntity> GetAll()
        {
            using (_context)
            {
                return _context.Addresses.ToList();
            }
        }

        public bool Create(AddressEntity address)
        {
            using (_context)
            {
                _context.Addresses.Add(address);
                _context.SaveChanges();
                return true;
            }
        }
    }
}