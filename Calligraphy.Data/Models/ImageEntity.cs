using System.ComponentModel.DataAnnotations;

namespace Calligraphy.Data.Models
{
    public class ImageEntity
    {
        [Key] public int Id { get; set; }
        public string ImageTitle { get; set; }
        public byte[] ImageData { get; set; }
    }
}