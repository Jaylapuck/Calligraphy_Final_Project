using System.Collections.Generic;
using Calligraphy.Data.Models;

namespace Calligraphy.Data.Repo.Image
{
    public interface IImageRepo
    {
        IEnumerable<ImageEntity> GetAll();
        
        ImageEntity GetById(int id);
        
        
        ImageEntity Add(ImageEntity image);
        
        ImageEntity Update(ImageEntity image);
        
        void DeleteById(int id);
    }
}