using System.Collections.Generic;
using System.Linq;
using Calligraphy.Data.Config;
using Calligraphy.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Calligraphy.Data.Repo.Customer
{
    public class CustomerRepo : ICustomerRepo
    {
        private readonly CalligraphyContext _context;


        public CustomerRepo(CalligraphyContext context)
        {
            _context = context;
        }

        public CustomerRepo()
        {
            _context = new CalligraphyContext();
        }

        public IEnumerable<CustomerEntity> GetAll()
        {
            using (_context)
            {
                var customers = _context.Customers
                    .Include(nameof(CustomerEntity.Address)).AsNoTracking().ToList();
                return customers;
            }
        }

        public bool Create(CustomerEntity customer)
        {
            using (_context)
            {
                _context.Customers.Add(customer);
                _context.SaveChanges();
                return true;
            }
        }
    }
}