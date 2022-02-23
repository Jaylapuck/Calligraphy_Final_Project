using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using Calligraphy.Business.Contract;
using Calligraphy.Business.Quote;
using Calligraphy.Data.Enums;
using Calligraphy.Data.Models;
using Calligraphy.Mailer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Calligraphy.Controllers
{
    [EnableCors("ApiCorsPolicy")]
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    [ValidateAntiForgeryToken]
    public class QuoteController : ControllerBase
    {
        private readonly IContractService _contractService;
        private readonly IMailerService _mailerService;
        private readonly IQuoteService _quoteService;
        private readonly IConfiguration _config;

        public QuoteController(IQuoteService quoteService, IMailerService mailerService,
            IContractService contractService, IConfiguration config)
        {
            _quoteService = quoteService;
            _mailerService = mailerService;
            _contractService = contractService;
            _config = config;
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
        [AllowAnonymous]
        public IActionResult Create([FromBody] QuoteEntity quote)
        {
            var result = _quoteService.Create(quote);
            if (result) return Ok(quote);
            return BadRequest();
        }

        // PUT
        [HttpPut]
        [Route("/api/Quote/{id:int}")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> Update([FromBody] QuoteEntity quote, int id)
        {
            if (quote.ApprovalStatus == Status.Approved)
            {
                var newContract = new ContractEntity();
                newContract.FinalCost = quote.Price;
                newContract.DownPayment = quote.Price / 2;
                newContract.DateCommissioned = DateTime.Now;
                newContract.EndDate = newContract.DateCommissioned.AddDays(quote.Duration);
                newContract.HasSignature = false;
                newContract.IsFinished = false;

                var quoteResult = _quoteService.Update(quote, id);
                var contractResult = _contractService.CreateNewContract(newContract);

                if (quoteResult.GetType() == typeof(OkObjectResult) && contractResult.GetType() == typeof(OkResult))
                {
                    quote.QuoteId = id;
                    var mailController = new MailController(_mailerService, _config);
                    await mailController.SendOwnerAlertNewContract(quote, newContract);
                }

                return quoteResult;
            }

            return _quoteService.Update(quote, id);
        }
    }
}