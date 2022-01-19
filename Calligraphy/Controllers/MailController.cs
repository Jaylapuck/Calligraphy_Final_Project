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

        public async Task<IActionResult> SendOwnerAlertNewQuote(FormEntity request)
        {
            var mailRequest = new MailRequest();

            var address = new MailAddress("tristanblacklafleur@hotmail.ca").Address;
            var emailTo = address;
            var subject = "New Quote for: " + request.Customer.FirstName + " " + request.Customer.LastName;
            var today = DateTime.UtcNow;
            var culture = new CultureInfo("en-US");
            var date = today.ToString(culture);
            var body = "<h1>A new request has been made, check the generated Quote</h1>";
            body += "<h3>New quote:</h3>";
            body += "<table style=\"border:1px solid black;\">";
            body += "<tr>";
            body += "<th>Estimated Cost</th>";
            body += "<th>Estimated Duration</th>";
            body += "<th>Materials</th>";
            body += "<th>Current Status</th>";
            body += "</tr>";
            body += "<tr>";
            body += "<td> $" + request.Quote.Price + "</td>";
            body += "<td>" + request.Quote.Duration + " Days</td>";
            body += "<td>" + request.Quote.Materials + "</td>";
            body += "<td>" + request.Quote.ApprovalStatus + "</td>";
            body += "</tr>";
            body += "</table>";
            body += "<br/><br/>";
            body += "Here is the date and time it was created: " + date;
            body += "<br/><br/>";
            body += "<h3>Here is the corresponding request:</h3>";
            body += "<table style=\"border:1px solid black;\">";
            body += "<tr>";
            body += "<th>First Name</th>";
            body += "<th>Last Name</th>";
            body += "<th>Street No.</th>";
            body += "<th>City</th>";
            body += "<th>Country</th>";
            body += "<th>Postal Code</th>";
            body += "<th>Service Type</th>";
            body += "</tr>";
            body += "<tr>";
            body += "<td>" + request.Customer.FirstName + "</td>";
            body += "<td>" + request.Customer.LastName + "</td>";
            body += "<td>" + request.Customer.Address.Street + "</td>";
            body += "<td>" + request.Customer.Address.City + "</td>";
            body += "<td>" + request.Customer.Address.Country + "</td>";
            body += "<td>" + request.Customer.Address.Postal + "</td>";
            body += "<td>" + request.ServiceType + "</td>";
            body += "</tr>";
            body += "</table>";
            body += "<br/>";
            body += "Email: " + request.Customer.Email;
            body += "<br/>";
            body += "Comments:";
            body += "<br/>";
            body += request.Comments;
            body += "<br/><br/>";
            body += "<h3>This is an auto-generated Quote, therefore it is encouraged to save this email and go to your admin panel to view the forms submitted and finalize the quote w/the customer</h3>";
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