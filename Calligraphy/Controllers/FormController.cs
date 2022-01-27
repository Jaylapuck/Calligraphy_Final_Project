using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using Calligraphy.Business.Form;
using Calligraphy.Data.Enums;
using Calligraphy.Data.Filters;
using Calligraphy.Data.Models;
using Calligraphy.Mailer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Calligraphy.Controllers
{
    [EnableCors("ApiCorsPolicy")]
    [ApiController]
    [Authorize]
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
        [AllowAnonymous]
        public IEnumerable<ServiceEntity> GetServices()
        {
            return _formService.GetAllServices();
        }

        // GET pageable
        [HttpGet]
        [Route("/api/form")]
        [Produces(MediaTypeNames.Application.Json)]
        public IActionResult GetAllPages([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var result = _formService.GetAll(filter, route);
            return result;
        }

        // POST: api/Form
        [HttpPost]
        [Route("/api/form")]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromForm] FormEntity form)
        {
            try
            {
                var quote = new QuoteEntity();
                quote.Materials = "None";
                quote.Price = form.StartingRate;
                switch (form.ServiceType)
                {
                    case ServiceType.Calligraphy:
                        quote.Duration = 14;
                        break;
                    case ServiceType.Engraving:
                        quote.Duration = 21;
                        break;
                    default:
                        quote.Duration = 0;
                        break;
                }

                form.Quote = quote;

                var result = _formService.Create(form);
                if (!result) return BadRequest();
                var mailController = new MailController(_mailService);
                await mailController.SendCustomerConfirmation(form);
                await mailController.SendOwnerAlertNewQuote(form);
                return Ok(form);
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