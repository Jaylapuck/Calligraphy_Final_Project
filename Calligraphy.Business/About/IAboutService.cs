using Calligraphy.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calligraphy.Business.About
{
    public interface IAboutService
    {
        // GET ALLs
        IActionResult Get();

        //UPDATE
        IActionResult Update(AboutEntity about);
    }
}