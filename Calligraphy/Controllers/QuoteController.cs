using Calligraphy.Business.Contract;
using Calligraphy.Business.Quote;
using Calligraphy.Data.Models;
using Calligraphy.Mailer.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Calligraphy.Controllers
{
        [EnableCors("ApiCorsPolicy")]
        [ApiController]
        [Route("[controller]")]
        public class QuoteController : ControllerBase
        {
        private readonly IQuoteService _quoteService;
        private readonly IMailerService _mailerService;
        private readonly IContractService _contractService;

        public QuoteController(IQuoteService quoteService, IMailerService mailerService, IContractService contractService)
        {
            _quoteService = quoteService;
            _mailerService = mailerService;
            _contractService = contractService;
        }

        // GET ALL
        [HttpGet]
        [Route("/api/Quote")]
        [Produces(MediaTypeNames.Application.Json)]
        public IEnumerable<QuoteEntity> GetAll()
        {
            return _quoteService.GetAll();
        }

        // GET BY FORM ID
        [HttpGet]
        [Route("/api/Quote/{id:int}")]
        [Produces(MediaTypeNames.Application.Json)]
        public IActionResult GetByFormId(int id)
        {
            return _quoteService.GetByFormId(id);
        }
        // POST
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        public IActionResult Create([FromBody] QuoteEntity quote)
        {
            var result = _quoteService.Create(quote);
            if (result)
            {
                return Ok(quote);
            }
            return BadRequest();
        }
        // PUT
        [HttpPut]
        [Route("/api/Quote/{id:int}")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> Update([FromBody] QuoteEntity quote, int id)
        {
            if(quote.ApprovalStatus == Data.Enums.Status.Approved)
            {
                var newContract = new ContractEntity();
                newContract.FinalCost = quote.Price;
                newContract.DownPayment = quote.Price / 2;
                newContract.DateCommissioned = System.DateTime.UtcNow;
                newContract.EndDate = newContract.DateCommissioned.AddDays(quote.Duration);
                newContract.HasSignature = false;
                newContract.IsFinished = false;

                _contractService.CreateNewContract(newContract);
                MailController mailController = new MailController(_mailerService);
                await mailController.SendOwnerAlertNewContract(quote, newContract);
            }

            return _quoteService.Update(quote, id);
        }
    }
}