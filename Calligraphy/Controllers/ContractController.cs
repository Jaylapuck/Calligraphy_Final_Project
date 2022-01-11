using Calligraphy.Business.Contract;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Calligraphy.Controllers
{
    [EnableCors("ApiCorsPolicy")]
    [ApiController]
    [Route("[controller]")]
    public class ContractController : ControllerBase
    {
        private IContractService _contractService;

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
        [Route("/api/contract/get/{ContractId:int")]
        public IActionResult GetContractById(int ContractId)
        {
            throw new NotImplementedException();
        }
    }
}
