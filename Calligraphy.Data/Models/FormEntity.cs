using System;
using Calligraphy.Data.Enums;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Calligraphy.Data.Models
{
    public class FormEntity
    {
        [ForeignKey("CustomerEntity, QuoteEntity")]
        [Key] public int FormId { get; set; }
        public virtual CustomerEntity Customer { get; set; }
        public ServiceType ServiceType { get; set; }
        public float StartingRate { get; set; }
        public string Comments { get; set; }
        
        public DateTime CreatedDate { get; set; }
        
        [NotMapped]
        public List<IFormFile> Attachments { get; set; }
        public virtual QuoteEntity Quote { get; set; }
    }
}
