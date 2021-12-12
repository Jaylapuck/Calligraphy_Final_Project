using Calligraphy.Business.Form;
using Calligraphy.Data.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;

namespace Calligraphy.Controllers
{
    [EnableCors("ApiCorsPolicy")]
    [ApiController]
    [Route("[controller]")]
    public class FormController : ControllerBase
    {
        // GET: Form
        private readonly IFormService _formService;
        
        public FormController(IFormService formService)
        {
            _formService = formService;
        }

        //public FormController()
        //{
        //    _formService  = new FormService();
        //}

        // GET: api/Form
        [HttpGet]
        [Route("/api/Form")]
        [Produces(MediaTypeNames.Application.Json)]
        public IEnumerable<FormEntity> Get()
        {
            return _formService.GetAll();
        }

        // POST: api/Form
        [HttpPost]
        [Route("/api/Form")]
        [Consumes(MediaTypeNames.Application.Json)]
        public IActionResult Post([FromBody] FormEntity form)
        {
            var result = _formService.Create(form);
            if (result)
            {
                return Ok(form);
            }
            return BadRequest();
        }
    }
}