using System;
using System.Collections.Generic;

using System.Data;

using ldvdbclasslibrary;

namespace ldvdbdal
{
    public class programdonorcommitmentsRepository : Repository<programdonorcommitments>, IprogramdonorcommitmentsRepository
    {
        private DbContext _context;
        public programdonorcommitmentsRepository(DbContext context)
            : base(context)
        {
            _context = context;
        }
        public long Create(programdonorcommitments newprogramdonorcommitments)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "INSERT INTO programdonorcommitments (DonorID, CommitmentDateTime, ClientRequestID, ReceivedAtCollectionPoint, DistributedToRecipient) VALUES (@DonorID, @CommitmentDateTime, @ClientRequestID, @ReceivedAtCollectionPoint, @DistributedToRecipient)";
                var DonorIDparam = command.CreateParameter();
                DonorIDparam.ParameterName = "@DonorID";
                DonorIDparam.Value = newprogramdonorcommitments.DonorID;
                command.Parameters.Add(DonorIDparam);
                var CommitmentDateTimeparam = command.CreateParameter();
                CommitmentDateTimeparam.ParameterName = "@CommitmentDateTime";
                CommitmentDateTimeparam.Value = newprogramdonorcommitments.CommitmentDateTime;
                command.Parameters.Add(CommitmentDateTimeparam);
                var ClientRequestIDparam = command.CreateParameter();
                ClientRequestIDparam.ParameterName = "@ClientRequestID";
                ClientRequestIDparam.Value = newprogramdonorcommitments.ClientRequestID;
                command.Parameters.Add(ClientRequestIDparam);
                var ReceivedAtCollectionPointparam = command.CreateParameter();
                ReceivedAtCollectionPointparam.ParameterName = "@ReceivedAtCollectionPoint";
                ReceivedAtCollectionPointparam.Value = newprogramdonorcommitments.ReceivedAtCollectionPoint;
                command.Parameters.Add(ReceivedAtCollectionPointparam);
                var DistributedToRecipientparam = command.CreateParameter();
                DistributedToRecipientparam.ParameterName = "@DistributedToRecipient";
                DistributedToRecipientparam.Value = newprogramdonorcommitments.DistributedToRecipient;
                command.Parameters.Add(DistributedToRecipientparam);
                command.ExecuteNonQuery();
                command.CommandText = "SELECT LAST_INSERT_ID(); ";
                return Convert.ToInt64(command.ExecuteScalar());
            }
        }
        public int Update(programdonorcommitments existingprogramdonorcommitments)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "UPDATE programdonorcommitments SET DonorID = @DonorID, CommitmentDateTime = @CommitmentDateTime, ClientRequestID = @ClientRequestID, ReceivedAtCollectionPoint = @ReceivedAtCollectionPoint, DistributedToRecipient = @DistributedToRecipient WHERE ProgramDonorCommitmentID = @ProgramDonorCommitmentID";
                var ProgramDonorCommitmentIDparam = command.CreateParameter();
                ProgramDonorCommitmentIDparam.ParameterName = "@ProgramDonorCommitmentID";
                ProgramDonorCommitmentIDparam.Value = existingprogramdonorcommitments.ProgramDonorCommitmentID;
                command.Parameters.Add(ProgramDonorCommitmentIDparam);
                var DonorIDparam = command.CreateParameter();
                DonorIDparam.ParameterName = "@DonorID";
                DonorIDparam.Value = existingprogramdonorcommitments.DonorID;
                command.Parameters.Add(DonorIDparam);
                var CommitmentDateTimeparam = command.CreateParameter();
                CommitmentDateTimeparam.ParameterName = "@CommitmentDateTime";
                CommitmentDateTimeparam.Value = existingprogramdonorcommitments.CommitmentDateTime;
                command.Parameters.Add(CommitmentDateTimeparam);
                var ClientRequestIDparam = command.CreateParameter();
                ClientRequestIDparam.ParameterName = "@ClientRequestID";
                ClientRequestIDparam.Value = existingprogramdonorcommitments.ClientRequestID;
                command.Parameters.Add(ClientRequestIDparam);
                var ReceivedAtCollectionPointparam = command.CreateParameter();
                ReceivedAtCollectionPointparam.ParameterName = "@ReceivedAtCollectionPoint";
                ReceivedAtCollectionPointparam.Value = existingprogramdonorcommitments.ReceivedAtCollectionPoint;
                command.Parameters.Add(ReceivedAtCollectionPointparam);
                var DistributedToRecipientparam = command.CreateParameter();
                DistributedToRecipientparam.ParameterName = "@DistributedToRecipient";
                DistributedToRecipientparam.Value = existingprogramdonorcommitments.DistributedToRecipient;
                command.Parameters.Add(DistributedToRecipientparam);
                return command.ExecuteNonQuery();
            }
        }
        public int Delete(programdonorcommitments existingprogramdonorcommitments)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "DELETE FROM programdonorcommitments WHERE ProgramDonorCommitmentID = @ProgramDonorCommitmentID";
                var ProgramDonorCommitmentIDparam = command.CreateParameter();
                ProgramDonorCommitmentIDparam.ParameterName = "@ProgramDonorCommitmentID";
                ProgramDonorCommitmentIDparam.Value = existingprogramdonorcommitments.ProgramDonorCommitmentID;
                command.Parameters.Add(ProgramDonorCommitmentIDparam);
                return command.ExecuteNonQuery();
            }
        }
        public programdonorcommitments RetrieveByID(long existingprogramdonorcommitmentsid)
        {
            programdonorcommitments ret = null;
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM programdonorcommitments WHERE ProgramDonorCommitmentID = @ProgramDonorCommitmentID";
                var ProgramDonorCommitmentIDparam = command.CreateParameter();
                ProgramDonorCommitmentIDparam.ParameterName = "@ProgramDonorCommitmentID";
                ProgramDonorCommitmentIDparam.Value = existingprogramdonorcommitmentsid;
                command.Parameters.Add(ProgramDonorCommitmentIDparam);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                     ret = new programdonorcommitments();
                     ret.ProgramDonorCommitmentID = reader.GetInt64(0);
                     ret.DonorID = reader.GetInt64(1);
                     ret.CommitmentDateTime = reader.GetDateTime(2);
                     ret.ClientRequestID = reader.GetInt64(3);
                     ret.ReceivedAtCollectionPoint = reader.GetInt64(4);
                     ret.DistributedToRecipient = reader.GetInt64(5);
                }
                return ret;
            }
        }
        public List<programdonorcommitments> RetrieveWithWhereClauseprogramdonorcommitments(string WhereClause)
        {
            var ret = new List<programdonorcommitments>();
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM programdonorcommitments WHERE " + WhereClause;
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                     var itemtoadd = new programdonorcommitments();
                     itemtoadd.ProgramDonorCommitmentID = reader.GetInt64(0);
                     itemtoadd.DonorID = reader.GetInt64(1);
                     itemtoadd.CommitmentDateTime = reader.GetDateTime(2);
                     itemtoadd.ClientRequestID = reader.GetInt64(3);
                     itemtoadd.ReceivedAtCollectionPoint = reader.GetInt64(4);
                     itemtoadd.DistributedToRecipient = reader.GetInt64(5);
                    ret.Add(itemtoadd);
                }
                return ret;
            }
        }
    }
}
