using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Calligraphy.Data.Models
{
    public class AboutEntity
    {
        [Key]
        public int AboutId { get; set; }

        //Personal
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Profession { get; set; }
        public string Description { get; set; }

        //Proffesional
        public string Language { get; set; }
        public string Country { get; set; }
        public string Experience { get; set; }

        //Mission
        public string Mission { get; set; }

    }

}