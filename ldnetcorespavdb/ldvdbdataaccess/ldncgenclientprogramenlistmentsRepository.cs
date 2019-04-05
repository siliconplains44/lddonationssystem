using System;
using System.Collections.Generic;

using System.Data;

using ldvdbclasslibrary;

namespace ldvdbdal
{
    public class clientprogramenlistmentsRepository : Repository<clientprogramenlistments>, IclientprogramenlistmentsRepository
    {
        private DbContext _context;
        public clientprogramenlistmentsRepository(DbContext context)
            : base(context)
        {
            _context = context;
        }
        public long Create(clientprogramenlistments newclientprogramenlistments)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "INSERT INTO clientprogramenlistments (ClientID, ProgramID) VALUES (@ClientID, @ProgramID)";
                var ClientIDparam = command.CreateParameter();
                ClientIDparam.ParameterName = "@ClientID";
                ClientIDparam.Value = newclientprogramenlistments.ClientID;
                command.Parameters.Add(ClientIDparam);
                var ProgramIDparam = command.CreateParameter();
                ProgramIDparam.ParameterName = "@ProgramID";
                ProgramIDparam.Value = newclientprogramenlistments.ProgramID;
                command.Parameters.Add(ProgramIDparam);
                command.ExecuteNonQuery();
                command.CommandText = "SELECT LAST_INSERT_ID(); ";
                return Convert.ToInt64(command.ExecuteScalar());
            }
        }
        public int Update(clientprogramenlistments existingclientprogramenlistments)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "UPDATE clientprogramenlistments SET ClientID = @ClientID, ProgramID = @ProgramID WHERE RecipientProgramEnlistmentID = @RecipientProgramEnlistmentID";
                var RecipientProgramEnlistmentIDparam = command.CreateParameter();
                RecipientProgramEnlistmentIDparam.ParameterName = "@RecipientProgramEnlistmentID";
                RecipientProgramEnlistmentIDparam.Value = existingclientprogramenlistments.RecipientProgramEnlistmentID;
                command.Parameters.Add(RecipientProgramEnlistmentIDparam);
                var ClientIDparam = command.CreateParameter();
                ClientIDparam.ParameterName = "@ClientID";
                ClientIDparam.Value = existingclientprogramenlistments.ClientID;
                command.Parameters.Add(ClientIDparam);
                var ProgramIDparam = command.CreateParameter();
                ProgramIDparam.ParameterName = "@ProgramID";
                ProgramIDparam.Value = existingclientprogramenlistments.ProgramID;
                command.Parameters.Add(ProgramIDparam);
                return command.ExecuteNonQuery();
            }
        }
        public int Delete(clientprogramenlistments existingclientprogramenlistments)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "DELETE FROM clientprogramenlistments WHERE RecipientProgramEnlistmentID = @RecipientProgramEnlistmentID";
                var RecipientProgramEnlistmentIDparam = command.CreateParameter();
                RecipientProgramEnlistmentIDparam.ParameterName = "@RecipientProgramEnlistmentID";
                RecipientProgramEnlistmentIDparam.Value = existingclientprogramenlistments.RecipientProgramEnlistmentID;
                command.Parameters.Add(RecipientProgramEnlistmentIDparam);
                return command.ExecuteNonQuery();
            }
        }
        public clientprogramenlistments RetrieveByID(long existingclientprogramenlistmentsid)
        {
            clientprogramenlistments ret = null;
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM clientprogramenlistments WHERE RecipientProgramEnlistmentID = @RecipientProgramEnlistmentID";
                var RecipientProgramEnlistmentIDparam = command.CreateParameter();
                RecipientProgramEnlistmentIDparam.ParameterName = "@RecipientProgramEnlistmentID";
                RecipientProgramEnlistmentIDparam.Value = existingclientprogramenlistmentsid;
                command.Parameters.Add(RecipientProgramEnlistmentIDparam);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                     ret = new clientprogramenlistments();
                     ret.RecipientProgramEnlistmentID = reader.GetInt64(0);
                     ret.ClientID = reader.GetInt64(1);
                     ret.ProgramID = reader.GetInt64(2);
                }
                return ret;
            }
        }
        public List<clientprogramenlistments> RetrieveWithWhereClauseclientprogramenlistments(string WhereClause)
        {
            var ret = new List<clientprogramenlistments>();
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM clientprogramenlistments WHERE " + WhereClause;
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                     var itemtoadd = new clientprogramenlistments();
                     itemtoadd.RecipientProgramEnlistmentID = reader.GetInt64(0);
                     itemtoadd.ClientID = reader.GetInt64(1);
                     itemtoadd.ProgramID = reader.GetInt64(2);
                    ret.Add(itemtoadd);
                }
                return ret;
            }
        }
    }
}
