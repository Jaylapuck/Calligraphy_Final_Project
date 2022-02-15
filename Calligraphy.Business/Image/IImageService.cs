using System.Collections.Generic;
using Calligraphy.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace Calligraphy.Business.Image
{
    public interface IImageService
    {
        // GET ALL
        IEnumerable<ImageEntity> GetAll();

        // GET BY ID
        IActionResult GetById(int id);

        IActionResult GetByImageId(int id);

        // CREATE
        IActionResult Create(ImageEntity image);

        // UPDATE
        IActionResult Update(ImageEntity image, int id);

        // DELETE
        IActionResult Delete(int id);
    }
}