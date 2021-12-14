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

        public IActionResult Update(ImageEntity image, int id)
        {
            if (image == null)
            {
                return new BadRequestResult();
            }
            var imageToUpdate = _imageRepo.GetById(id);
            if (imageToUpdate == null)
            {
                return new NotFoundResult();
            }
            imageToUpdate.ImageTitle = image.ImageTitle;
            imageToUpdate.ImageData = image.ImageData;
            _imageRepo.Update(imageToUpdate);
            return new OkObjectResult(imageToUpdate);
        }


        public IActionResult Delete(int id)
        {
            var image = _imageRepo.GetById(id);
            if (image == null)
            {
                return new NotFoundResult();
            }
            _imageRepo.DeleteById(id);
            return new OkObjectResult(image);
        }
    }
}