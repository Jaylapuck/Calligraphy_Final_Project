using Calligraphy.Data.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Calligraphy.Data.Repo.Contract
{
    public interface IContractRepo
    {
        IEnumerable<ContractEntity> GetAll();
        ContractEntity GetById(int ContractId);
        int CreateNewContract(ContractEntity NewEntity);
    }
}
