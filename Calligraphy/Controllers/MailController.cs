using Calligraphy.Data.Models;
using Calligraphy.Mailer.Model;
using Calligraphy.Mailer.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Calligraphy.Controllers
{
    public class MailController : ControllerBase
    {
        private readonly IMailerService _mailService;

        public MailController(IMailerService mailService)
        {
            _mailService = mailService;
        }

        public async Task<IActionResult> SendCustomerConfirmation(FormEntity request)
        {
            var mailRequest = new MailRequest();

            var address = new MailAddress(request.Customer.Email).Address;
            var emailTo = address;
            var subject = "Request for: " + request.ServiceType;
            var today = DateTime.UtcNow;
            var culture = new CultureInfo("en-US");
            var date = today.ToString(culture);
            var body = "<h1>Greetings from Serene Flourish!</h1>";
            body += "<h3>Hello " + request.Customer.FirstName + "!</h3>";
            body += "<p>We\'ve received your order and will contact you as soon as your package is shipped. You can find your purchase information below.</p>";
            body += "<h3>Order Summary</h3>";
            body += "<p>" + date + "</p>";
            body += "<h3>Service Title</h3>";
            body += "<p>" + request.ServiceType + "</p>";
            body += "<h3>Service Approximate Rate</h3>";
            body += "<p>$" + request.StartingRate + "/hr</p>";
            body += "<h3>Customization Comments</h3>";
            body += "<p>" + request.Comments + "</p>";
            body += "<h3>Your Contact Information</h3>";
            body += "<p>" + request.Customer.FirstName + " " + request.Customer.LastName + "<p>";
            body += "<p>Address: " + request.Customer.Address.Street + " " + request.Customer.Address.City + " " + request.Customer.Address.Country + " " + request.Customer.Address.Postal + "</p>";
            body += "<p>Email: " + request.Customer.Email + "</p>";
            body += "<h3>This is a auto-generated Quote and may be subject to change. If there are any changes we encounter, we will contact you again to receive your approval.</h3>";
            var file = request.Attachments;

            mailRequest.email = emailTo;
            mailRequest.subject = subject;
            mailRequest.body = body;
            mailRequest.attachtments = file;

            await _mailService.SendMailAsync(mailRequest);
            return Ok();
        }
    }
}
