using System;
using System.Collections.Generic;
using System.Text;

namespace ldvdbbusinesslogic
{
    public class AppConfiguration
    {
        public string SendGridApiKey { get; set; }
        public string TwilioaccountSid { get; set; }
        public string TwilioAuthToken { get; set; }
        public string FromEmailAddress { get; set; }
        public string FromSmsPhoneNumber { get; set; }
    }
}
