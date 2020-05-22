using System;
using System.Collections.Generic;
using System.Text;

namespace NotificationService.Options
{
    public class EmailOptions
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
        public bool SmtpEnableSsl { get; set; }
    }
}
