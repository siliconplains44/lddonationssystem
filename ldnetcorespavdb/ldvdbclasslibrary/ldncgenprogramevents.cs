using System;

namespace ldvdbclasslibrary
{
    public class programevents
    {
        public long ProgramEventID { get; set; } 
        public long ProgramID { get; set; } 
        public long IsSingleDate { get; set; } 
        public DateTime FromDate { get; set; } 
        public DateTime ToDate { get; set; } 
        public string Description { get; set; } 
        public string Name { get; set; } 
    }
}
