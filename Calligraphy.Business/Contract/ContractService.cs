using System.Linq;
using Calligraphy.Data.Models;
using Calligraphy.Data.Repo.Contract;
using Microsoft.AspNetCore.Mvc;

namespace Calligraphy.Business.Contract
{
    public class ContractService : IContractService
    {
        private readonly IContractRepo _contractRepo;

        public ContractService(IContractRepo contractRepo)
        {
            _contractRepo = contractRepo;
        }

        public IActionResult CreateNewContract(ContractEntity ContractEntity)
        {
            var CreateContract = _contractRepo.CreateNewContract(ContractEntity);
            if (CreateContract != 0)
                return new OkResult();
            return new BadRequestResult();
        }

        public IActionResult GetAllContracts()
        {
            var contracts = _contractRepo.GetAll();
            if (contracts != null)
                return new OkObjectResult(contracts);
            return new NotFoundResult();
        }

        public IActionResult GetContractById(int ContractId)
        {
            var contract = _contractRepo.GetById(ContractId);
            if (contract != null)
                return new OkObjectResult(contract);
            return new NotFoundResult();
        }

        public IActionResult GetContractsByMonthOfYear(int Month, int Year, bool IsFinished)
        {
            var MonthlyContracts = _contractRepo.GetByMonthOfYear(Month, Year, IsFinished);

            if (MonthlyContracts.Count() == 0) return new OkResult();

            return new OkObjectResult(MonthlyContracts);
        }

        public IActionResult UpdateContract(ContractEntity ContractEntity)
        {
            var UpdatedContract = _contractRepo.UpdateContract(ContractEntity);
            if (UpdatedContract != null)
                return new OkObjectResult(UpdatedContract);
            return new BadRequestResult();
        }
    }
}