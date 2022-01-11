using Calligraphy.Business.Quote;
using Calligraphy.Data.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Mime;

namespace Calligraphy.Controllers
{
        [EnableCors("ApiCorsPolicy")]
        [ApiController]
        [Route("[controller]")]
        public class QuoteController : ControllerBase
        {
        private readonly IQuoteService _quoteService;

        public QuoteController(IQuoteService quoteService)
        {
            _quoteService = quoteService;
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
        public IActionResult Update([FromBody] QuoteEntity quote, int id)
        {
            return _quoteService.Update(quote, id);
        }
    }
}