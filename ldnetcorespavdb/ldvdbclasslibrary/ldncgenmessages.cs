using System;

namespace ldvdbclasslibrary
{
    public class messages
    {
        public long MessageID { get; set; } 
        public long FromAccountID { get; set; } 
        public long FromAccountTypeID { get; set; } 
        public DateTime MessageSentDateTime { get; set; } 
        public string Subject { get; set; } 
        public string Body { get; set; } 
    }
}
