using System;

namespace ldvdbclasslibrary
{
    public class programdonorcommitments
    {
        public long ProgramDonorCommitmentID { get; set; } 
        public long DonorID { get; set; } 
        public DateTime CommitmentDateTime { get; set; } 
        public long ClientRequestID { get; set; } 
        public long ReceivedAtCollectionPoint { get; set; } 
        public long DistributedToRecipient { get; set; } 
    }
}
