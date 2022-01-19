using Calligraphy.Business.Form;
using Calligraphy.Business.Quote;
using Calligraphy.Data.Models;
using Calligraphy.Mailer.Model;
using Calligraphy.Mailer.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;
using Calligraphy.Data.Filters;
using Calligraphy.Data.Repo.Wrappers;
using Org.BouncyCastle.Ocsp;

namespace Calligraphy.Controllers
{
    [EnableCors("ApiCorsPolicy")]
    [ApiController]
    [Route("[controller]")]
    public class FormController : ControllerBase
    {
        private readonly IFormService _formService;
        private readonly IMailerService _mailService;

        public FormController(IFormService formService, IMailerService mailService)
        {
            _formService = formService;
            _mailService = mailService;
        }
        
        [HttpGet]
        [Route("/api/Form/Services")]
        [Produces(MediaTypeNames.Application.Json)]
        public IEnumerable<ServiceEntity> GetServices()
        {
            return _formService.GetAllServices();
        }
        
        // GET pageable
        [HttpGet]
        [Route("/api/Form")]
        [Produces(MediaTypeNames.Application.Json)]
        public IActionResult GetAllPages([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var result = _formService.GetAll(filter, route);
            return result;
        }
        
        // POST: api/Form
        [HttpPost("/api/Form")]
        public async Task<IActionResult> Post([FromForm] FormEntity form)
        {
            try
            {
                var quote = new QuoteEntity();
                quote.Materials = "None";
                quote.Price = form.StartingRate;
                form.Quote = quote;

                var result = _formService.Create(form);
                if (result)
                {


                    //form.Quote.FormId = form.FormId;
                    //form.Quote.Price = form.StartingRate;
                    //quote.Form = form;
                    //_quoteService.Create(quote);
                    var mailController = new MailController(_mailService);
                    await mailController.SendCustomerConfirmation(form);
                    await mailController.SendOwnerAlertNewQuote(form);
                    return Ok(form);
                }
                return BadRequest();
            }
            catch (ArgumentException nullExc)
            {
                throw new ArgumentException("Email not found", nullExc);
            }
            catch (FormatException formatExc)
            {
                throw new FormatException("Not a valid email", formatExc);
            }
        }
    }
}