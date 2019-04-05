using System;

namespace ldvdbclasslibrary
{
    public class moduleviews
    {
        public long ModuleViewID { get; set; } 
        public string Name { get; set; } 
        public DateTime Occurred { get; set; } 
        public long? LoggedInSecurityUserID { get; set; } 
    }
}
