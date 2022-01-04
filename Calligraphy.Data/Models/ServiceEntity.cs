using Calligraphy.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Calligraphy.Data.Models
{
    public class ServiceEntity
    {
        [Key]
        public int ServiceId { get; set; }
        public ServiceType TypeName { get; set; }
        public float StartingRate { get; set; }
    }
}
