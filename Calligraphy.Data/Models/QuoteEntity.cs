using Calligraphy.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Calligraphy.Data.Models
{

    public class QuoteEntity
    {
        [ForeignKey("FormEntity")]
        [Key] public int QuoteId { get; set; }
        public virtual FormEntity Form { get; set; }
        public float Price { get; set; }
        public string Materials { get; set; }
        public Status ApprovalStatus { get; set; }

    }

}
