using System.Collections.Generic;
using System.Net.Mime;
using Calligraphy.Business.Form;
using Calligraphy.Business.Image;
using Calligraphy.Data.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Calligraphy.Controllers
{
    [EnableCors("ApiCorsPolicy")]
    [ApiController]
    [Route("[controller]")]
    public class ImageController : Controller
    {
        private readonly IImageService _formService;
        
        public ImageController(IImageService formService)
        {
            _formService = formService;
        }
        
        // GET ALL
        [HttpGet]
        [Route("/api/Form")]
        [Produces(MediaTypeNames.Application.Json)]
        public IEnumerable<ImageEntity> GetAll()
        {
            return _formService.GetAll();
        }
       
        // GET BY ID
        [HttpGet]
        [Route("/api/Form/{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        public IActionResult GetById(int id)
        {
            return _formService.GetById(id);
        }
        
        // POST
        [HttpPost]
        [Route("/api/Form")]
        [Produces(MediaTypeNames.Application.Json)]
        public IActionResult Create([FromBody] ImageEntity image)
        {
           return _formService.Create(image);
        }
        
        // PUT
        [HttpPut]
        [Route("/api/Form")]
        [Produces(MediaTypeNames.Application.Json)]
        public IActionResult Update([FromBody] ImageEntity image)
        {
            return _formService.Update(image);
        }

        [HttpDelete]
        [Route("/api/Form/{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        public IActionResult Delete(ImageEntity image)
        {
            return _formService.Delete(image);
        }
    }
}