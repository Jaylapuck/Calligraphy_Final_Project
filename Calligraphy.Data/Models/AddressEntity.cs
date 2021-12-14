﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Calligraphy.Data.Models
{
    public class AddressEntity
    {
        [Key] public int AddressId { get; set; }
        public string Street { get; set; }
        public string Postal { get; set; }

        public string City { get; set; }
        public string Country { get; set; }
    }
}
