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
        
        public void AddImage(ImageEntity image)
        {
            _context.Images.Add(image);
            _context.SaveChanges();
        }
        
        public void DeleteImage(int id)
        {
            var image = _context.Images.FirstOrDefault(i => i.Id == id);
            _context.Images.Remove(image);
            _context.SaveChanges();
        }
        
        public ImageEntity GetImage(int id)
        {
            return _context.Images.FirstOrDefault(i => i.Id == id);
        }
        
        public List<ImageEntity> GetImages()
        {
            return _context.Images.ToList();
        }
        
        public void UpdateImage(ImageEntity image)
        {
            _context.Images.Update(image);
            _context.SaveChanges();
        }
    }
}