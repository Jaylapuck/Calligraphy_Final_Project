using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calligraphy.Data.Models;
using Calligraphy.Data.Repo;

namespace Calligraphy.Business.Form
{
    public class FormService :  IFormService
    {
        private readonly IFormRepo _formRepo;
        
        public FormService(IFormRepo formRepo)
        {
            _formRepo = formRepo;
        }

        public FormService()
        {
            _formRepo = new FormRepo();
        }
        
        public IEnumerable<FormEntity> GetAll()
        {
            return _formRepo.GetAll();
        }

        //Add new form
        public bool Create(FormEntity form)
        {
            return _formRepo.Create(form);
        }
       
    }
}
