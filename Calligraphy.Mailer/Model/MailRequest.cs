using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

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