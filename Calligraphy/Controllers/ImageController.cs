using System.Collections.Generic;
using System.Net.Mime;
using Calligraphy.Business.Form;
using Calligraphy.Business.Image;
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
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;
        
        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        // GET ALL
        [HttpGet]
        [Route("/api/Image")]
        [Produces(MediaTypeNames.Application.Json)]
        [AllowAnonymous]
        public IEnumerable<ImageEntity> GetAll()
        {
            return _imageService.GetAll();
        }
       
        // GET BY ID
        [HttpGet]
        [Route("/api/Image/{id:int}")]
        [Produces(MediaTypeNames.Application.Json)]
        [AllowAnonymous]
        public IActionResult GetById(int id)
        {
            return _imageService.GetById(id);
        }
        
        // GET BY ID
        [HttpGet]
        [Route("/api/Image/portfolio/{id:int}")]
        [Produces(MediaTypeNames.Application.Json)]
        [AllowAnonymous]
        public IActionResult GetByImageId(int id)
        {
            return _imageService.GetByImageId(id);
        }
        
        // POST
        [HttpPost]
        [Route("/api/Image")]
        [Consumes(MediaTypeNames.Application.Json)]
        public IActionResult Create([FromBody] ImageEntity image)
        {
            return _imageService.Create(image);
        }
        
        // PUT
        [HttpPut]
        [Route("/api/Image/{id:int}")]
        [Consumes(MediaTypeNames.Application.Json)]
        public IActionResult Update([FromBody] ImageEntity image, int id)
        {
           return _imageService.Update(image, id);
        }

        [HttpDelete]
        [Route("/api/Image/{id:int}")]
        [Produces(MediaTypeNames.Application.Json)]
        public IActionResult Delete(int id)
        {
            return _imageService.Delete(id);

        }
    }
}