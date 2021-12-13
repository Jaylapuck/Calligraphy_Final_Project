using System.Collections.Generic;
using Calligraphy.Data.Models;
using Calligraphy.Data.Repo.Image;

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

        public ImageEntity GetById(int id)
        {
            return _imageRepo.GetById(id);
        }

        public void Create(ImageEntity image)
        {
            _imageRepo.Add(image);
        }

        public void Update(ImageEntity image)
        {
            _imageRepo.Update(image);
        }

        public void Delete(int id)
        {
            _imageRepo.Delete(id);
        }
    }
}