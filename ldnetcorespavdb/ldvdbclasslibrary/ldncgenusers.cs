using System;

namespace ldvdbclasslibrary
{
    public class users
    {
        public long UserID { get; set; } 
        public string Email { get; set; } 
        public string Password { get; set; } 
        public long IsActive { get; set; } 
        public string RegistrationCode { get; set; } 
        public string PasswordResetCode { get; set; } 
        public long AccountTypeID { get; set; } 
    }
}
