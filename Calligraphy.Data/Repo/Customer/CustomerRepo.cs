using Calligraphy.Data.Config;
using Calligraphy.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Calligraphy.Data.Repo.Customer
{
    public class CustomerRepo : ICustomerRepo
    {
        private readonly CalligraphyContext _context;
        private readonly DbContextOptions<CalligraphyContext> options;


        public CustomerRepo(CalligraphyContext context)
        {
            _context = context;
        }

        public CustomerRepo()
        {
            _context = new CalligraphyContext(options);
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
