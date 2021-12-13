using System.ComponentModel.DataAnnotations;

namespace Calligraphy.Data.Models
{
    public class FormEntity
    {
        [Key] public int FormId { get; set; }

        public string ServiceType { get; set; }
        public string Comments { get; set; }
    }
}
