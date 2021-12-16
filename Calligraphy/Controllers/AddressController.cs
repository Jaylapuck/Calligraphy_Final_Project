using Calligraphy.Business.Address;
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
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        // GET: api/Address
        [HttpGet]
        [Route("/api/Address")]
        [Produces(MediaTypeNames.Application.Json)]
        public IEnumerable<AddressEntity> Get()
        {
            return _addressService.GetAll();
        }

        // POST: api/Address
        [HttpPost]
        [Route("/api/Address")]
        [Consumes(MediaTypeNames.Application.Json)]
        public IActionResult Post([FromBody] AddressEntity address)
        {
            var result = _addressService.Create(address);
            if (result)
            {
                return Ok(address);
            }
            return BadRequest();
        }
    }
}