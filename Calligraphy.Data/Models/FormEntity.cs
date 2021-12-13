using Calligraphy.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calligraphy.Data.Models
{
    public class FormEntity
    {
        [Key] public int FormId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public AddressEntity Address { get; set; }
        public ServiceType ServiceType { get; set; }
        public string Comments { get; set; }
    }
}
