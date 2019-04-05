using System;
using System.Collections.Generic;

using System.Data;

using ldvdbclasslibrary;

namespace ldvdbdal
{
    public class donorfundscommitmentsRepository : Repository<donorfundscommitments>, IdonorfundscommitmentsRepository
    {
        private DbContext _context;
        public donorfundscommitmentsRepository(DbContext context)
            : base(context)
        {
            _context = context;
        }
        public long Create(donorfundscommitments newdonorfundscommitments)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "INSERT INTO donorfundscommitments (Occured, DonorID, Amount, Received) VALUES (@Occured, @DonorID, @Amount, @Received)";
                var Occuredparam = command.CreateParameter();
                Occuredparam.ParameterName = "@Occured";
                Occuredparam.Value = newdonorfundscommitments.Occured;
                command.Parameters.Add(Occuredparam);
                var DonorIDparam = command.CreateParameter();
                DonorIDparam.ParameterName = "@DonorID";
                DonorIDparam.Value = newdonorfundscommitments.DonorID;
                command.Parameters.Add(DonorIDparam);
                var Amountparam = command.CreateParameter();
                Amountparam.ParameterName = "@Amount";
                Amountparam.Value = newdonorfundscommitments.Amount;
                command.Parameters.Add(Amountparam);
                var Receivedparam = command.CreateParameter();
                Receivedparam.ParameterName = "@Received";
                Receivedparam.Value = newdonorfundscommitments.Received;
                command.Parameters.Add(Receivedparam);
                command.ExecuteNonQuery();
                command.CommandText = "SELECT LAST_INSERT_ID(); ";
                return Convert.ToInt64(command.ExecuteScalar());
            }
        }
        public int Update(donorfundscommitments existingdonorfundscommitments)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "UPDATE donorfundscommitments SET Occured = @Occured, DonorID = @DonorID, Amount = @Amount, Received = @Received WHERE DonorFundsCommitmentID = @DonorFundsCommitmentID";
                var DonorFundsCommitmentIDparam = command.CreateParameter();
                DonorFundsCommitmentIDparam.ParameterName = "@DonorFundsCommitmentID";
                DonorFundsCommitmentIDparam.Value = existingdonorfundscommitments.DonorFundsCommitmentID;
                command.Parameters.Add(DonorFundsCommitmentIDparam);
                var Occuredparam = command.CreateParameter();
                Occuredparam.ParameterName = "@Occured";
                Occuredparam.Value = existingdonorfundscommitments.Occured;
                command.Parameters.Add(Occuredparam);
                var DonorIDparam = command.CreateParameter();
                DonorIDparam.ParameterName = "@DonorID";
                DonorIDparam.Value = existingdonorfundscommitments.DonorID;
                command.Parameters.Add(DonorIDparam);
                var Amountparam = command.CreateParameter();
                Amountparam.ParameterName = "@Amount";
                Amountparam.Value = existingdonorfundscommitments.Amount;
                command.Parameters.Add(Amountparam);
                var Receivedparam = command.CreateParameter();
                Receivedparam.ParameterName = "@Received";
                Receivedparam.Value = existingdonorfundscommitments.Received;
                command.Parameters.Add(Receivedparam);
                return command.ExecuteNonQuery();
            }
        }
        public int Delete(donorfundscommitments existingdonorfundscommitments)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "DELETE FROM donorfundscommitments WHERE DonorFundsCommitmentID = @DonorFundsCommitmentID";
                var DonorFundsCommitmentIDparam = command.CreateParameter();
                DonorFundsCommitmentIDparam.ParameterName = "@DonorFundsCommitmentID";
                DonorFundsCommitmentIDparam.Value = existingdonorfundscommitments.DonorFundsCommitmentID;
                command.Parameters.Add(DonorFundsCommitmentIDparam);
                return command.ExecuteNonQuery();
            }
        }
        public donorfundscommitments RetrieveByID(long existingdonorfundscommitmentsid)
        {
            donorfundscommitments ret = null;
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM donorfundscommitments WHERE DonorFundsCommitmentID = @DonorFundsCommitmentID";
                var DonorFundsCommitmentIDparam = command.CreateParameter();
                DonorFundsCommitmentIDparam.ParameterName = "@DonorFundsCommitmentID";
                DonorFundsCommitmentIDparam.Value = existingdonorfundscommitmentsid;
                command.Parameters.Add(DonorFundsCommitmentIDparam);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                     ret = new donorfundscommitments();
                     ret.DonorFundsCommitmentID = reader.GetInt64(0);
                     ret.Occured = reader.IsDBNull(1) ? (DateTime?)null : reader.GetDateTime(1);
                     ret.DonorID = reader.GetInt64(2);
                     ret.Amount = reader.GetDecimal(3);
                     ret.Received = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4);
                }
                return ret;
            }
        }
        public List<donorfundscommitments> RetrieveWithWhereClausedonorfundscommitments(string WhereClause)
        {
            var ret = new List<donorfundscommitments>();
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM donorfundscommitments WHERE " + WhereClause;
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                     var itemtoadd = new donorfundscommitments();
                     itemtoadd.DonorFundsCommitmentID = reader.GetInt64(0);
                     itemtoadd.Occured = reader.GetDateTime(1);
                     itemtoadd.DonorID = reader.GetInt64(2);
                     itemtoadd.Amount = reader.GetDecimal(3);
                     itemtoadd.Received = reader.GetDateTime(4);
                    ret.Add(itemtoadd);
                }
                return ret;
            }
        }
    }
}
