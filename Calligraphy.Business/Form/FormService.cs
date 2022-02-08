#nullable enable
using System;
using System.Collections.Generic;
using Calligraphy.Data.Models;
using Calligraphy.Data.Pagination;
using Calligraphy.Data.Repo.Form;
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
        
        public PagedList<FormEntity> GetAll(FormParameters formParameters)
        {
            var formEntities = _formRepo.GetAll(formParameters);
            return formEntities;
        }
        
        public IEnumerable<ServiceEntity> GetAllServices()
        {
            return _serviceRepo.GetAll();
        }

        //Add new form
        public bool Create(FormEntity? form)
        {
            // check if form is valid
            if (form == null)
            {
                return false;
            }
            
            form.CreatedDate = DateTime.Now;
            return _formRepo.Create(form);
        }
       
    }
}
