using Calligraphy.Business.Customer;
using Calligraphy.Data.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;

namespace Calligraphy.Controllers
{
    [EnableCors("ApiCorsPolicy")]
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET: api/Customer
        [HttpGet]
        [Route("/api/Customer")]
        [Produces(MediaTypeNames.Application.Json)]
        [Authorize]
        public IEnumerable<CustomerEntity> Get()
        {
            return _customerService.GetAll();
        }

        // POST: api/Customer
        [HttpPost]
        [Route("/api/Customer")]
        [Consumes(MediaTypeNames.Application.Json)]
        public IActionResult Post([FromBody] CustomerEntity customer)
        {
            var result = _customerService.Create(customer);
            if (result)
            {
                return Ok(customer);
            }
            return BadRequest();
        }
    }
}