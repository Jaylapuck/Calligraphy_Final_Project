using System.ComponentModel.DataAnnotations;
using Calligraphy.Data.Enums;

namespace Calligraphy.Data.Models
{
    public class ServiceEntity
    {
        [Key] public int ServiceId { get; set; }

        public ServiceType TypeName { get; set; }
        public float StartingRate { get; set; }
    }
}