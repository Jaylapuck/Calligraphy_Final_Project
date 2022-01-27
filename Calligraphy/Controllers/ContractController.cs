using Calligraphy.Business.Contract;
using Calligraphy.Data.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Calligraphy.Controllers
{
    [EnableCors("ApiCorsPolicy")]
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ContractController : ControllerBase
    {
        private readonly IContractService _contractService;

        public ContractController(IContractService contractService)
        {
            _contractService = contractService;
        }

        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [Route("/api/contract/get")]
        public IActionResult GetAllContracts()
        {
            return _contractService.GetAllContracts();
        }

        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [Route("/api/contract/get/{ContractId:int}")]
        public IActionResult GetContractById(int ContractId)
        {
            return _contractService.GetContractById(ContractId);
        }

        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [Route("/api/contract/get/{Month:int}/{Year:int}/{IsFinished:bool}")]
        public IActionResult GetContractsByMonthOfYear(int Month, int Year, bool IsFinished = true)
        {
            return _contractService.GetContractsByMonthOfYear(Month, Year, IsFinished);
        }

        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [Route("/api/contract/update")]
        public IActionResult UpdateContract([FromBody] ContractEntity Contract)
        {
            return _contractService.UpdateContract(Contract);
        }
    }
}
