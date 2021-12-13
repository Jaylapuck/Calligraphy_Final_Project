﻿using System.Collections.Generic;
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
            using (_context)
            {
                return _context.Images.ToList();
            }
        }

        public ImageEntity GetById(int id)
        {
            using (_context)
            {
                return _context.Images.FirstOrDefault(x => x.Id == id);
            }
        }

        public ImageEntity Add(ImageEntity image)
        {
            using (_context)
            {
                _context.Images.Add(image);
                _context.SaveChanges();
                return image;
            }
        }
        
        public ImageEntity Update(ImageEntity image)
        {
            using (_context)
            {
                _context.Images.Update(image);
                _context.SaveChanges();
                return image;
            }
        }

        public void DeleteById(int id)
        {
            using (_context)
            {
                var image = _context.Images.FirstOrDefault(x => x.Id == id);
                _context.Images.Remove(image);
                _context.SaveChanges();
            }
        }
    }
}