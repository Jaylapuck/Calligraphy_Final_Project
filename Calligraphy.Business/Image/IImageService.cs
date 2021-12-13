using System.Collections.Generic;
using Calligraphy.Data.Models;

namespace Calligraphy.Business.Image
{
    public interface IImageService
    {
        // GET ALL
        IEnumerable<ImageEntity> GetAll();
        
        // GET BY ID
        ImageEntity GetById(int id);
        
        // CREATE
        void Create(ImageEntity image);
        
        // UPDATE
        void Update(ImageEntity image);
        
        // DELETE
        void Delete(int id);
        
    }
}