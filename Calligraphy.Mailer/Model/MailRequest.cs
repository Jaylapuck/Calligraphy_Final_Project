using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Author: Tristan Lafleur
/// 
/// Simple model class to map the contents of the payload form the frontend
/// </summary>

namespace Calligraphy.Mailer.Model
{
    public class MailRequest
    {
        public string email { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
        public List<IFormFile> attachtments { get; set; }
    }
}