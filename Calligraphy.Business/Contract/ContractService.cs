using Calligraphy.Data.Models;
using Calligraphy.Data.Repo.Contract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            int CreateContract = _contractRepo.CreateNewContract(ContractEntity);
            if(CreateContract != 0)
            {
                return new OkResult();
            }
            else
            {
                return new BadRequestResult();
            }
        }

        public IActionResult GetAllContracts()
        {
            var contracts = _contractRepo.GetAll();
            if(contracts != null)
            {
                return new OkObjectResult(contracts);
            }
            else
            {
                return new NotFoundResult();
            }
        }

        public IActionResult GetContractById(int ContractId)
        {
            var contract = _contractRepo.GetById(ContractId);
            if (contract != null)
            {
                return new OkObjectResult(contract);
            }
            else
            {
                return new NotFoundResult();
            }
        }

        public IActionResult GetContractsByMonthOfYear(int Month, int Year, bool IsFinished)
        {
            throw new NotImplementedException();
        }

        public IActionResult UpdateContract(ContractEntity ContractEntity)
        {
            var UpdatedContract = _contractRepo.UpdateContract(ContractEntity);
            if(UpdatedContract != null)
            {
                return new OkObjectResult(UpdatedContract);
            }
            else
            {
                return new BadRequestResult();
            }
        }
    }
}
