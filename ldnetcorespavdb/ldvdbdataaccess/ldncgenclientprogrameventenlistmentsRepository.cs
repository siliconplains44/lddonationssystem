using System;
using System.Collections.Generic;

using System.Data;

using ldvdbclasslibrary;

namespace ldvdbdal
{
    public class clientprogrameventenlistmentsRepository : Repository<clientprogrameventenlistments>, IclientprogrameventenlistmentsRepository
    {
        private DbContext _context;
        public clientprogrameventenlistmentsRepository(DbContext context)
            : base(context)
        {
            _context = context;
        }
        public long Create(clientprogrameventenlistments newclientprogrameventenlistments)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "INSERT INTO clientprogrameventenlistments (ClientID, ProgramEventID) VALUES (@ClientID, @ProgramEventID)";
                var ClientIDparam = command.CreateParameter();
                ClientIDparam.ParameterName = "@ClientID";
                ClientIDparam.Value = newclientprogrameventenlistments.ClientID;
                command.Parameters.Add(ClientIDparam);
                var ProgramEventIDparam = command.CreateParameter();
                ProgramEventIDparam.ParameterName = "@ProgramEventID";
                ProgramEventIDparam.Value = newclientprogrameventenlistments.ProgramEventID;
                command.Parameters.Add(ProgramEventIDparam);
                command.ExecuteNonQuery();
                command.CommandText = "SELECT LAST_INSERT_ID(); ";
                return Convert.ToInt64(command.ExecuteScalar());
            }
        }
        public int Update(clientprogrameventenlistments existingclientprogrameventenlistments)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "UPDATE clientprogrameventenlistments SET ClientID = @ClientID, ProgramEventID = @ProgramEventID WHERE ClientProgramEventEnlistementID = @ClientProgramEventEnlistementID";
                var ClientProgramEventEnlistementIDparam = command.CreateParameter();
                ClientProgramEventEnlistementIDparam.ParameterName = "@ClientProgramEventEnlistementID";
                ClientProgramEventEnlistementIDparam.Value = existingclientprogrameventenlistments.ClientProgramEventEnlistementID;
                command.Parameters.Add(ClientProgramEventEnlistementIDparam);
                var ClientIDparam = command.CreateParameter();
                ClientIDparam.ParameterName = "@ClientID";
                ClientIDparam.Value = existingclientprogrameventenlistments.ClientID;
                command.Parameters.Add(ClientIDparam);
                var ProgramEventIDparam = command.CreateParameter();
                ProgramEventIDparam.ParameterName = "@ProgramEventID";
                ProgramEventIDparam.Value = existingclientprogrameventenlistments.ProgramEventID;
                command.Parameters.Add(ProgramEventIDparam);
                return command.ExecuteNonQuery();
            }
        }
        public int Delete(clientprogrameventenlistments existingclientprogrameventenlistments)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "DELETE FROM clientprogrameventenlistments WHERE ClientProgramEventEnlistementID = @ClientProgramEventEnlistementID";
                var ClientProgramEventEnlistementIDparam = command.CreateParameter();
                ClientProgramEventEnlistementIDparam.ParameterName = "@ClientProgramEventEnlistementID";
                ClientProgramEventEnlistementIDparam.Value = existingclientprogrameventenlistments.ClientProgramEventEnlistementID;
                command.Parameters.Add(ClientProgramEventEnlistementIDparam);
                return command.ExecuteNonQuery();
            }
        }
        public clientprogrameventenlistments RetrieveByID(long existingclientprogrameventenlistmentsid)
        {
            clientprogrameventenlistments ret = null;
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM clientprogrameventenlistments WHERE ClientProgramEventEnlistementID = @ClientProgramEventEnlistementID";
                var ClientProgramEventEnlistementIDparam = command.CreateParameter();
                ClientProgramEventEnlistementIDparam.ParameterName = "@ClientProgramEventEnlistementID";
                ClientProgramEventEnlistementIDparam.Value = existingclientprogrameventenlistmentsid;
                command.Parameters.Add(ClientProgramEventEnlistementIDparam);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                     ret = new clientprogrameventenlistments();
                     ret.ClientProgramEventEnlistementID = reader.GetInt64(0);
                     ret.ClientID = reader.GetInt64(1);
                     ret.ProgramEventID = reader.GetInt64(2);
                }
                return ret;
            }
        }
        public List<clientprogrameventenlistments> RetrieveWithWhereClauseclientprogrameventenlistments(string WhereClause)
        {
            var ret = new List<clientprogrameventenlistments>();
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM clientprogrameventenlistments WHERE " + WhereClause;
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                     var itemtoadd = new clientprogrameventenlistments();
                     itemtoadd.ClientProgramEventEnlistementID = reader.GetInt64(0);
                     itemtoadd.ClientID = reader.GetInt64(1);
                     itemtoadd.ProgramEventID = reader.GetInt64(2);
                    ret.Add(itemtoadd);
                }
                return ret;
            }
        }
    }
}
