using System;
using System.Collections.Generic;
using System.Text;

namespace ldvdbclasslibrary
{
    public class displaymessage : messages
    {
        public List<messagerecipients> listMessageRecipients { get; set; }
        public long messageRecipientId { get; set; }
        public DateTime? messageRead { get; set; }
        public string FromLastName { get; set; }
        public string FromFirstName { get; set; }
    }
}
