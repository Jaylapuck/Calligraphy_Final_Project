using Calligraphy.Data.Enums;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Calligraphy.Data.Models
{
    public class FormEntity
    {
        [ForeignKey("CustomerEntity")]
        [Key] public int FormId { get; set; }
        public virtual CustomerEntity Customer { get; set; }
        public ServiceType ServiceType { get; set; }
        public double StartingRate { get; set; }
        public string Comments { get; set; }
        [NotMapped]
        public List<IFormFile> Attachments { get; set; }
    }
}
