﻿using System.Collections.Generic;
using Calligraphy.Data.Models;

namespace Calligraphy.Data.Repo.Image
{
    public interface IImageRepo
    {
        IEnumerable<ImageEntity> GetAll();
        
        ImageEntity GetById(int id);
        
        
        void Add(ImageEntity image);
        
        void Update(ImageEntity image);
        
        void Delete(int id);
    }
}