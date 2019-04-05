using System;
using System.Collections.Generic;
using System.Text;

namespace ldvdbclasslibrary
{
    public class displayclientrequest
    {
        public long ClientRequestID { get; set; }
        public long ProgramDonorCommittmentID { get; set; }
        public string ClientRequest { get; set; }
        public string ProgramName { get; set; }
        public string ProgramDescription { get; set; }
        public long ProgramYear { get; set; }
        public string ClientLastName { get; set; }
        public string ClientMiddleName { get; set; }
        public string ClientFirstName { get; set; }
        public DateTime ClientBirthDate { get; set; }
        public string ClientCity { get; set; }
        public string ClientZip { get; set; }
    }
}
