using System.Net.Mime;
using Calligraphy.Business.About;
using Calligraphy.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Calligraphy.Controllers
{
    [EnableCors("ApiCorsPolicy")]
    [ApiController]
    [Route("[controller]")]
    public class AboutController : ControllerBase
    {
        private readonly IAboutService _aboutService;

        public AboutController(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }

        // GET
        [HttpGet]
        [Route("/api/About")]
        [Produces(MediaTypeNames.Application.Json)]
        public IActionResult Get()
        {
            return _aboutService.Get();
        }

        // PUT
        [HttpPut]
        [Route("/api/About")]
        [Authorize]
        [Consumes(MediaTypeNames.Application.Json)]
        public IActionResult Update([FromBody] AboutEntity about)
        {
            return _aboutService.Update(about);
        }
    }
}