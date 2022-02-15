using System.Collections.Generic;
using Calligraphy.Data.Models;

namespace Calligraphy.Data.Repo.Contract
{
    public interface IContractRepo
    {
        IEnumerable<ContractEntity> GetAll();
        ContractEntity GetById(int ContractId);
        IEnumerable<ContractEntity> GetByMonthOfYear(int Month, int Year, bool IsFinished);
        int CreateNewContract(ContractEntity NewEntity);
        ContractEntity UpdateContract(ContractEntity Entity);
    }
}