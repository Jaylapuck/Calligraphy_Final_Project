using System.Collections.Generic;
using Calligraphy.Data.Models;
using Calligraphy.Data.Repo.Image;
using Microsoft.AspNetCore.Mvc;

namespace Calligraphy.Business.Image
{
    public class ImageService : IImageService
    {
        private readonly IImageRepo _imageRepo;
        
        public ImageService(IImageRepo imageRepo)
        {
            _imageRepo = imageRepo;
        }

        public ImageService()
        {
            _imageRepo = new ImageRepo();
        }

        public IEnumerable<ImageEntity> GetAll()
        {
            return _imageRepo.GetAll();
        }

        public IActionResult GetById(int id)
        {
            var image = _imageRepo.GetById(id);
            if (image == null)
            {
                return new NotFoundResult();
            }
            return new OkObjectResult(image);
        }

        public IActionResult Create(ImageEntity image)
        {
            if (image == null)
            {
                return new BadRequestResult();
            }
            _imageRepo.Add(image);
            return new OkObjectResult(image);
        }
        
        public IActionResult Update(ImageEntity image)
        {
            if (image == null)
            {
                return new BadRequestResult();
            }
            _imageRepo.Update(image);
            return new OkObjectResult(image);
        }

        public IActionResult Delete(ImageEntity image)
        {
            if (image == null)
            {
                return new NotFoundResult(); 
            }
            _imageRepo.Delete(image);
            return new OkResult();
        }
    }
}