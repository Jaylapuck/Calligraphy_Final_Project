﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calligraphy.Data.Models;
using Calligraphy.Data.Repo;
using Calligraphy.Data.Repo.Service;

namespace Calligraphy.Business.Form
{
    public class FormService :  IFormService
    {
        private readonly IFormRepo _formRepo;
        private readonly IServiceRepo _serviceRepo;
        
        public FormService(IFormRepo formRepo, IServiceRepo serviceRepo)
        {
            _formRepo = formRepo;
            _serviceRepo = serviceRepo;
        }

        public FormService()
        {
            _formRepo = new FormRepo();
        }
        
        public IEnumerable<FormEntity> GetAll()
        {
            return _formRepo.GetAll();
        }

        public IEnumerable<ServiceEntity> GetAllServices()
        {
            return _serviceRepo.GetAll();
        }

        //Add new form
        public bool Create(FormEntity form)
        {
            return _formRepo.Create(form);
        }
       
    }
}
