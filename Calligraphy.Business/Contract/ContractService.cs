﻿using Calligraphy.Data.Models;
using Calligraphy.Data.Repo.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calligraphy.Business.Contract
{
    public class ContractService : IContractService
    {
        private IContractRepo _contractRepo;

        public ContractService(IContractRepo contractRepo)
        {
            _contractRepo = contractRepo;
        }

        public List<ContractEntity> GetAllContracts()
        {
            throw new NotImplementedException();
        }
    }
}
