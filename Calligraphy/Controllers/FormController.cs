using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using Calligraphy.Business.Form;
using Calligraphy.Data.Enums;
using Calligraphy.Data.Models;
using Calligraphy.Data.Pagination;
using Calligraphy.Mailer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Calligraphy.Controllers
{
    [EnableCors("ApiCorsPolicy")]
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class FormController : ControllerBase
    {
        private readonly IFormService _formService;
        private readonly ILogger<FormController> _logger;
        private readonly IMailerService _mailService;
        private readonly IConfiguration _config;

        public FormController(IFormService formService, IMailerService mailService, ILogger<FormController> logger, IConfiguration config)
        {
            _formService = formService;
            _mailService = mailService;
            _logger = logger;
            _config = config;
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
        [Route("/api/forms")]
        [Produces(MediaTypeNames.Application.Json)]
        public IActionResult GetAllPages([FromQuery] FormParameters formParameters)
        {
            var forms = _formService.GetAll(formParameters);
            var metadata = new
            {
                forms.TotalCount,
                forms.PageSize,
                forms.CurrentPage,
                forms.TotalPages,
                forms.HasNext,
                forms.HasPrevious
            };

            // make it accessible from the frontend
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            _logger.LogInformation($"Returning {forms.Count} forms");
            return Ok(forms);
        }

        // POST: api/Form
        [HttpPost]
        [Route("/api/form")]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromForm] FormEntity form)
        {
            try
            {
                var quote = new QuoteEntity
                {
                    Materials = "None",
                    Price = form.StartingRate,
                    Duration = form.ServiceType switch
                    {
                        ServiceType.Calligraphy => 14,
                        ServiceType.Engraving => 21,
                        _ => 0
                    }
                };
                form.Quote = quote;
                var result = _formService.Create(form);
                if (!result) return BadRequest();
                var mailController = new MailController(_mailService, _config);
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