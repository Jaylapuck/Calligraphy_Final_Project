using System.ComponentModel.DataAnnotations;
using Calligraphy.Data.Enums;

namespace Calligraphy.Data.Models
{
    public class QuoteEntity
    {
        [Key] public int QuoteId { get; set; }
        public float Price { get; set; }
        public int Duration { get; set; }
        public string Materials { get; set; }
        public Status ApprovalStatus { get; set; }
    }
}