using System.Collections.Generic;
using System.Linq;
using Calligraphy.Data.Config;
using Calligraphy.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Calligraphy.Data.Repo.Image
{
    public class ImageRepo : IImageRepo
    {
        private readonly CalligraphyContext _context;
        private readonly DbContextOptions<CalligraphyContext> options;


        public ImageRepo(CalligraphyContext context)
        {
            _context = context;
        }

        public ImageRepo()
        {
            _context = new CalligraphyContext(options);
        }

        public IEnumerable<ImageEntity> GetAll()
        {
            return _context.Images.ToList();
        }

        public ImageEntity GetById(int id)
        {
            return _context.Images.FirstOrDefault(x => x.Id == id);
        }
        
        public ImageEntity GetByImageId(int imageId)
        {
            return _context.Images.FirstOrDefault(x => x.ImageId == imageId);
        }

        public ImageEntity Add(ImageEntity image)
        {
            _context.Images.Add(image);
            _context.SaveChanges();
            return image;
        }
        
        public ImageEntity Update(ImageEntity image)
        {
            _context.Entry(image).State = EntityState.Modified;
            _context.SaveChanges();
            return image;
        }

        public void DeleteById(int id)
        {
            var image = _context.Images.FirstOrDefault(x => x.ImageId == id);
            _context.Images.Remove(image);
            _context.SaveChanges();
        }
    }
}