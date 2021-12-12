using System;
using System.Collections.Generic;
using System.Linq;

namespace Calligraphy.Controllers
{
    public class HomeController : Microsoft.AspNetCore.Mvc.Controller
    {
        public Microsoft.AspNetCore.Mvc.ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
