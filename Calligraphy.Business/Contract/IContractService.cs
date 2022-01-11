using Calligraphy.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calligraphy.Business.Contract
{
    interface IContractService
    {
        IActionResult GetAllContracts();
        IActionResult GetContractById(int ContractId);
        IActionResult CreateNewContract(ContractEntity ContractEntity);
        IActionResult UpdateContract(ContractEntity ContractEntity);
    }
}
