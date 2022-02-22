using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Calligraphy.Data.Models.AuthenticationModels.JWT.JWT
{
    public class AdminEntity
    {
        [JsonIgnore] [Key] public int Id { get; set; }

        [Required] public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string RefreshToken { get; set; }
        
        public DateTime RefreshTokenExpirationDate { get; set; }
    }
}