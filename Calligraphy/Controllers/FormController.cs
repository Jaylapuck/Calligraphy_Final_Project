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
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Org.BouncyCastle.Ocsp;

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
        [HttpPost("/api/form")]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromForm] FormEntity form)
        {
            try
            {
                var quote = new QuoteEntity();
                var address = new MailAddress(form.Customer.Email).Address;
                var mailRequest = new MailRequest();

                var emailTo = address;
                var subject = "Request for: " + form.ServiceType;
                var today = DateTime.UtcNow;
                var culture = new CultureInfo("en-US");
                var date = today.ToString(culture);
                var body = "<h1>Greetings from Serene Flourish!</h1>";
                body += "<h3>Hello " + form.Customer.FirstName + "!</h3>";
                body += "<p>We\'ve received your order and will contact you as soon as your package is shipped. You can find your purchase information below.</p>";
                body += "<h3>Order Summary</h3>";
                body += "<p>" + date + "</p>";
                body += "<h3>Service Title</h3>";
                body += "<p>" + form.ServiceType + "</p>";
                body += "<h3>Service Approximate Rate</h3>";
                body += "<p>$" + form.StartingRate + "/hr</p>";
                body += "<h3>Customization Comments</h3>";
                body += "<p>" + form.Comments + "</p>";
                body += "<h3>Your Contact Information</h3>";
                body += "<p>" + form.Customer.FirstName + " " + form.Customer.LastName + "<p>";
                body += "<p>Address: " + form.Customer.Address.Street + " " + form.Customer.Address.City + " " + form.Customer.Address.Country + " " + form.Customer.Address.Postal + "</p>";
                body += "<p>Email: " + form.Customer.Email + "</p>";
                body += "<h3>This is a auto-generated Quote and may be subject to change. If there are any changes we encounter, we will contact you again to receive your approval.</h3>";
                var file = form.Attachments;

                mailRequest.email = emailTo;
                mailRequest.subject = subject;
                mailRequest.body = body;
                mailRequest.attachtments = file;

               
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
                    await _mailService.SendMailAsync(mailRequest);
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