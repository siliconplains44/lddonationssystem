using System;
namespace ldvdbclasslibrary
{
    public class displaydonor
    {
        public long DonorID { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string FirstName { get; set; }        
        public DateTime BirthDate { get; set; }
        public string MobilePhoneNumber { get; set; }
        public string HomePhoneNumber { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

        public displaydonor()
        {
           
        }
    }
}
