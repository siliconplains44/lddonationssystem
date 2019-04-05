using System;
using System.Collections.Generic;

using System.Data;

using ldvdbclasslibrary;

namespace ldvdbdal
{
    public class clientrequestsRepository : Repository<clientrequests>, IclientrequestsRepository
    {
        private DbContext _context;
        public clientrequestsRepository(DbContext context)
            : base(context)
        {
            _context = context;
        }
        public long Create(clientrequests newclientrequests)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "INSERT INTO clientrequests (ClientID, RequestInformation, ProgramID) VALUES (@ClientID, @RequestInformation, @ProgramID)";
                var ClientIDparam = command.CreateParameter();
                ClientIDparam.ParameterName = "@ClientID";
                ClientIDparam.Value = newclientrequests.ClientID;
                command.Parameters.Add(ClientIDparam);
                var RequestInformationparam = command.CreateParameter();
                RequestInformationparam.ParameterName = "@RequestInformation";
                RequestInformationparam.Value = newclientrequests.RequestInformation;
                command.Parameters.Add(RequestInformationparam);
                var ProgramIDparam = command.CreateParameter();
                ProgramIDparam.ParameterName = "@ProgramID";
                ProgramIDparam.Value = newclientrequests.ProgramID;
                command.Parameters.Add(ProgramIDparam);
                command.ExecuteNonQuery();
                command.CommandText = "SELECT LAST_INSERT_ID(); ";
                return Convert.ToInt64(command.ExecuteScalar());
            }
        }
        public int Update(clientrequests existingclientrequests)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "UPDATE clientrequests SET ClientID = @ClientID, RequestInformation = @RequestInformation, ProgramID = @ProgramID WHERE ClientRequestID = @ClientRequestID";
                var ClientRequestIDparam = command.CreateParameter();
                ClientRequestIDparam.ParameterName = "@ClientRequestID";
                ClientRequestIDparam.Value = existingclientrequests.ClientRequestID;
                command.Parameters.Add(ClientRequestIDparam);
                var ClientIDparam = command.CreateParameter();
                ClientIDparam.ParameterName = "@ClientID";
                ClientIDparam.Value = existingclientrequests.ClientID;
                command.Parameters.Add(ClientIDparam);
                var RequestInformationparam = command.CreateParameter();
                RequestInformationparam.ParameterName = "@RequestInformation";
                RequestInformationparam.Value = existingclientrequests.RequestInformation;
                command.Parameters.Add(RequestInformationparam);
                var ProgramIDparam = command.CreateParameter();
                ProgramIDparam.ParameterName = "@ProgramID";
                ProgramIDparam.Value = existingclientrequests.ProgramID;
                command.Parameters.Add(ProgramIDparam);
                return command.ExecuteNonQuery();
            }
        }
        public int Delete(clientrequests existingclientrequests)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "DELETE FROM clientrequests WHERE ClientRequestID = @ClientRequestID";
                var ClientRequestIDparam = command.CreateParameter();
                ClientRequestIDparam.ParameterName = "@ClientRequestID";
                ClientRequestIDparam.Value = existingclientrequests.ClientRequestID;
                command.Parameters.Add(ClientRequestIDparam);
                return command.ExecuteNonQuery();
            }
        }
        public clientrequests RetrieveByID(long existingclientrequestsid)
        {
            clientrequests ret = null;
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM clientrequests WHERE ClientRequestID = @ClientRequestID";
                var ClientRequestIDparam = command.CreateParameter();
                ClientRequestIDparam.ParameterName = "@ClientRequestID";
                ClientRequestIDparam.Value = existingclientrequestsid;
                command.Parameters.Add(ClientRequestIDparam);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                     ret = new clientrequests();
                     ret.ClientRequestID = reader.GetInt64(0);
                     ret.ClientID = reader.IsDBNull(1) ? (long?)null : reader.GetInt64(1);
                     ret.RequestInformation = reader.IsDBNull(2) ? null : reader.GetString(2);
                     ret.ProgramID = reader.IsDBNull(3) ? (long?)null : reader.GetInt64(3);
                }
                return ret;
            }
        }
        public List<clientrequests> RetrieveWithWhereClauseclientrequests(string WhereClause)
        {
            var ret = new List<clientrequests>();
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM clientrequests WHERE " + WhereClause;
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                     var itemtoadd = new clientrequests();
                     itemtoadd.ClientRequestID = reader.GetInt64(0);
                     itemtoadd.ClientID = reader.GetInt64(1);
                     itemtoadd.RequestInformation = reader.GetString(2);
                     itemtoadd.ProgramID = reader.GetInt64(3);
                    ret.Add(itemtoadd);
                }
                return ret;
            }
        }
    }
}
