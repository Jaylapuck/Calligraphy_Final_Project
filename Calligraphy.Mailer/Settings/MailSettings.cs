

/// <summary>
/// Author: Tristan lafleur
/// 
/// Simple model class that appsettings will use to
/// create and instance of the SMTP configuration
/// 
/// </summary>

namespace Calligraphy.Mailer.Settings
{
    public class MailSettings
    {
        public string mail { get; set; }
        public string displayName { get; set; }
        public string password { get; set; }
        public string host { get; set; }
        public int port { get; set; }
    }
}