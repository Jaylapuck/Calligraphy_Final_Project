﻿using Calligraphy.Data.Config;
using Calligraphy.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
            using (_context)
            {
                return _context.Contracts.ToList();
            }
        }

        public ContractEntity GetById(int ContractId)
        {
            throw new NotImplementedException();
        }
    }
}
