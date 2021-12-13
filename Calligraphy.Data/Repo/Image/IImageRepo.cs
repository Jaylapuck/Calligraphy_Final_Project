using System.Collections.Generic;
using Calligraphy.Data.Models;

namespace Calligraphy.Data.Repo.Image
{
    public interface IImageRepo
    {
        IEnumerable<ImageEntity> GetAll();
        
        ImageEntity GetById(int id);
        
        
        bool Add(ImageEntity image);
        
        bool Update(ImageEntity image);
        
        bool Delete(ImageEntity image);
    }
}