using System;
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
            //verify that  the image entity is valid
            if (image == null)
            {
                return new BadRequestObjectResult("Image entity is null");
            }
            
            //verify that the imageId is not already in the database
            var imageInDb = _imageRepo.GetByImageId(image.ImageId);
            
            if (imageInDb != null)
            {
                return new BadRequestObjectResult("ImageId already exists");
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