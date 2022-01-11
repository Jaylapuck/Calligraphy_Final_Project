﻿using Calligraphy.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calligraphy.Business.Contract
{
    interface IContractService
    {
        IEnumerable<ContractEntity> GetAllContracts();
        ContractEntity GetContractById(int ContractId);
    }
}