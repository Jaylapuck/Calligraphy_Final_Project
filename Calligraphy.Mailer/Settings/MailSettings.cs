using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calligraphy.Mailer.Settings
{
    public class MailSettings
    {
        public string mail { get; set; }
        public string displayName { get; set; }
        public string password { get; set; }
        public string host { get; set; }
        public int port { get; set; }

        //public MailSettings(string m, string dN, string pa, string h, int po)
        //{
        //    this.mail = m;
        //    this.displayName = dN;
        //    this.password = pa;
        //    this.host = h;
        //    this.port = po;
        //}
    }
}
