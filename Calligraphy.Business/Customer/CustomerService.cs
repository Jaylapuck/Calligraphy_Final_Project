using System.Collections.Generic;
using Calligraphy.Data.Models;
using Calligraphy.Data.Repo.Customer;

namespace Calligraphy.Business.Customer
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepo _customerRepo;

        public CustomerService(ICustomerRepo customerRepo)
        {
            _customerRepo = customerRepo;
        }

        public CustomerService()
        {
            _customerRepo = new CustomerRepo();
        }

        public IEnumerable<CustomerEntity> GetAll()
        {
            return _customerRepo.GetAll();
        }

        //Add new form
        public bool Create(CustomerEntity form)
        {
            return _customerRepo.Create(form);
        }
    }
}