using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Calligraphy.Data.Models
{
    public class CustomerEntity
    {
        [ForeignKey("AddressEntity")]
        [Key] public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public virtual AddressEntity Address { get; set; }

    }
}
