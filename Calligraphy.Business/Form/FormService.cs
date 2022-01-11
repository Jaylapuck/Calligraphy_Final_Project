#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calligraphy.Data.Filters;
using Calligraphy.Data.Helpers;
using Calligraphy.Data.IUriService;
using Calligraphy.Data.Models;
using Calligraphy.Data.Repo;
using Calligraphy.Data.Repo.Form;
using Calligraphy.Data.Repo.Service;
using Calligraphy.Data.Repo.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Calligraphy.Business.Form
{
    public class FormService :  IFormService
    {
        private readonly IFormRepo _formRepo;
        private readonly IServiceRepo _serviceRepo;
        private readonly IUriService  _serviceUri;
        
        public FormService(IFormRepo formRepo, IServiceRepo serviceRepo, IUriService serviceUri)
        {
            _formRepo = formRepo;
            _serviceRepo = serviceRepo;
            _serviceUri = serviceUri;
        }
        
        public IActionResult GetAll(PaginationFilter filter, string? route)
        {
           
            
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var data = _formRepo.GetAll(validFilter, out var totalRecords);
            var pagedResponse = PaginationHelper.CreatePagedReponse<FormEntity>(data, validFilter,  totalRecords, _serviceUri,  route);
            return new OkObjectResult(pagedResponse);
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
