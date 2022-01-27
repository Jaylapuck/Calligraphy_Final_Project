using System.Collections.Generic;
using System.Net.Mime;
using Calligraphy.Business.Customer;
using Calligraphy.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Calligraphy.Controllers
{
    [EnableCors("ApiCorsPolicy")]
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class CustomerController : ControllerBase
    {
        // Should be removed later on
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET: api/Customer
        [HttpGet]
        [Route("/api/Customer")]
        [Produces(MediaTypeNames.Application.Json)]
        [AllowAnonymous]
        public IEnumerable<CustomerEntity> Get()
        {
            return _customerService.GetAll();
        }

        // POST: api/Customer
        [HttpPost]
        [Route("/api/Customer")]
        [Consumes(MediaTypeNames.Application.Json)]
        [AllowAnonymous]
        public IActionResult Post([FromBody] CustomerEntity customer)
        {
            var result = _customerService.Create(customer);
            if (result) return Ok(customer);
            return BadRequest();
        }
    }
}