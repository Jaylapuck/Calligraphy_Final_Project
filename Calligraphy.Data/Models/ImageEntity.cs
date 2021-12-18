using System.ComponentModel.DataAnnotations;

namespace Calligraphy.Data.Models
{
    public class ImageEntity
    {
        [Key] public int Id { get; set; }
        
        [Required] public int ImageId { get; set; }
        public string ImageTitle { get; set; }
        [Required] public string ImageData { get; set; }
    }
}