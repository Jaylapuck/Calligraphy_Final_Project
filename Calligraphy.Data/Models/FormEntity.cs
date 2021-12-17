﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Calligraphy.Data.Models
{
    public class FormEntity
    {
        [ForeignKey("CustomerEntity")]
        [Key] public int FormId { get; set; }
        public virtual CustomerEntity Customer { get; set; }
        public string ServiceType { get; set; }
        public string Comments { get; set; }
    }
}
