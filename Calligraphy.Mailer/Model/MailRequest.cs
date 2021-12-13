using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calligraphy.Mailer.Model
{
    public class MailRequest
    {
        public string email { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
        public List<IFormFile> attachtments { get; set; }

        public MailRequest(string e, string s, string b, List<IFormFile> a)
        {
            this.email = e;
            this.subject = s;
            this.body = b;
            this.attachtments = a;
        }
    }
}
