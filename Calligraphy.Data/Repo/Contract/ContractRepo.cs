using Calligraphy.Data.Config;
using Calligraphy.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Calligraphy.Data.Repo.Contract
{
    public class ContractRepo : IContractRepo
    {
        private readonly CalligraphyContext _context;
        private readonly DbContextOptions<CalligraphyContext> options;

        public ContractRepo(CalligraphyContext context)
        {
            _context = context;
        }

        public ContractRepo()
        {
            _context = new CalligraphyContext(options);
        }

        public IEnumerable<ContractEntity> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
