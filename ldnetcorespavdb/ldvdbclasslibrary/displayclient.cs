using System;
using System.Collections.Generic;
using System.Text;

namespace ldvdbclasslibrary
{
    public class displayclient
    {
        public long ClientID { get; set; }
        public string EmailAddress { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string FirstName { get; set; }
        public long FatherIndividualID { get; set; }
        public long MotherIndividualID { get; set; }
        public DateTime BirthDate { get; set; }
        public string MobilePhoneNumber { get; set; }
        public string HomePhoneNumber { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }
}
