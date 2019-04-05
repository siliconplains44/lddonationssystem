using System;

namespace ldvdbclasslibrary
{
    public class messagerecipients
    {
        public long MessageRecipientID { get; set; } 
        public long MessageID { get; set; } 
        public long AccountID { get; set; } 
        public long AccountTypeID { get; set; } 
        public DateTime? MessageRead { get; set; } 
    }
}
