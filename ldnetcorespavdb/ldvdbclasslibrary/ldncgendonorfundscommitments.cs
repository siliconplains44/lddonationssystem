using System;

namespace ldvdbclasslibrary
{
    public class donorfundscommitments
    {
        public long DonorFundsCommitmentID { get; set; } 
        public DateTime? Occured { get; set; } 
        public long DonorID { get; set; } 
        public decimal Amount { get; set; } 
        public DateTime? Received { get; set; } 
    }
}
