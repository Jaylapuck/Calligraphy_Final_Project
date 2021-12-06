using Calligraphy.Business.Form;
using Calligraphy.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;

namespace Calligraphy.Controllers
{
    public class FormController : ApiController
    {
        // GET: Form
        private readonly IFormService _formService;
        
        public FormController(IFormService formService)
        {
            _formService = formService;
        }

        public FormController()
        {
            _formService  = new FormService();
        }

        // GET: api/Form
        [HttpGet]
        public IEnumerable<FormEntity> Get()
        {
            return _formService.GetAll();
        }

        // POST: api/Form
        [HttpPost]
        public IHttpActionResult Post([FromBody] FormEntity form)
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