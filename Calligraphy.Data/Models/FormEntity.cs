using System.ComponentModel.DataAnnotations;

namespace Calligraphy.Data.Models
{
    public class FormEntity
    {
        [Key] public int FormId { get; set; }

        public CustomerEntity Customer { get; set; }

        public string ServiceType { get; set; }
        public string Comments { get; set; }
    }
}
