using System;

namespace ldvdbclasslibrary
{
    public class notificationsettings
    {
        public long NotificationSettingID { get; set; } 
        public long AccountID { get; set; } 
        public long AccountTypeID { get; set; } 
        public long EnableEmailMessages { get; set; } 
        public long EnableSMSMessages { get; set; } 
    }
}
