using Calligraphy.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Calligraphy.Data.Models
{
    public class ServiceEntity
    {
        public int ServiceId { get; set; }
        public ServiceType TypeName { get; set; }
        public float StartingRate { get; set; }
    }
}
