using Calligraphy.Data.Models;
using Microsoft.AspNetCore.Mvc;

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