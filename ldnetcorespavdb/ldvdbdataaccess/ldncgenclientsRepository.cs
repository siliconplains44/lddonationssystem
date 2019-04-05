using System;
using System.Collections.Generic;

using System.Data;

using ldvdbclasslibrary;

namespace ldvdbdal
{
    public class clientsRepository : Repository<clients>, IclientsRepository
    {
        private DbContext _context;
        public clientsRepository(DbContext context)
            : base(context)
        {
            _context = context;
        }
        public long Create(clients newclients)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "INSERT INTO clients (UserID, IndividualID, IsDeleted) VALUES (@UserID, @IndividualID, @IsDeleted)";
                var UserIDparam = command.CreateParameter();
                UserIDparam.ParameterName = "@UserID";
                UserIDparam.Value = newclients.UserID;
                command.Parameters.Add(UserIDparam);
                var IndividualIDparam = command.CreateParameter();
                IndividualIDparam.ParameterName = "@IndividualID";
                IndividualIDparam.Value = newclients.IndividualID;
                command.Parameters.Add(IndividualIDparam);
                var IsDeletedparam = command.CreateParameter();
                IsDeletedparam.ParameterName = "@IsDeleted";
                IsDeletedparam.Value = newclients.IsDeleted;
                command.Parameters.Add(IsDeletedparam);
                command.ExecuteNonQuery();
                command.CommandText = "SELECT LAST_INSERT_ID(); ";
                return Convert.ToInt64(command.ExecuteScalar());
            }
        }
        public int Update(clients existingclients)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "UPDATE clients SET UserID = @UserID, IndividualID = @IndividualID, IsDeleted = @IsDeleted WHERE ClientID = @ClientID";
                var ClientIDparam = command.CreateParameter();
                ClientIDparam.ParameterName = "@ClientID";
                ClientIDparam.Value = existingclients.ClientID;
                command.Parameters.Add(ClientIDparam);
                var UserIDparam = command.CreateParameter();
                UserIDparam.ParameterName = "@UserID";
                UserIDparam.Value = existingclients.UserID;
                command.Parameters.Add(UserIDparam);
                var IndividualIDparam = command.CreateParameter();
                IndividualIDparam.ParameterName = "@IndividualID";
                IndividualIDparam.Value = existingclients.IndividualID;
                command.Parameters.Add(IndividualIDparam);
                var IsDeletedparam = command.CreateParameter();
                IsDeletedparam.ParameterName = "@IsDeleted";
                IsDeletedparam.Value = existingclients.IsDeleted;
                command.Parameters.Add(IsDeletedparam);
                return command.ExecuteNonQuery();
            }
        }
        public int Delete(clients existingclients)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "DELETE FROM clients WHERE ClientID = @ClientID";
                var ClientIDparam = command.CreateParameter();
                ClientIDparam.ParameterName = "@ClientID";
                ClientIDparam.Value = existingclients.ClientID;
                command.Parameters.Add(ClientIDparam);
                return command.ExecuteNonQuery();
            }
        }
        public clients RetrieveByID(long existingclientsid)
        {
            clients ret = null;
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM clients WHERE ClientID = @ClientID";
                var ClientIDparam = command.CreateParameter();
                ClientIDparam.ParameterName = "@ClientID";
                ClientIDparam.Value = existingclientsid;
                command.Parameters.Add(ClientIDparam);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                     ret = new clients();
                     ret.ClientID = reader.GetInt64(0);
                     ret.UserID = reader.GetInt64(1);
                     ret.IndividualID = reader.GetInt64(2);
                     ret.IsDeleted = reader.GetInt64(3);
                }
                return ret;
            }
        }
        public List<clients> RetrieveWithWhereClauseclients(string WhereClause)
        {
            var ret = new List<clients>();
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM clients WHERE " + WhereClause;
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                     var itemtoadd = new clients();
                     itemtoadd.ClientID = reader.GetInt64(0);
                     itemtoadd.UserID = reader.GetInt64(1);
                     itemtoadd.IndividualID = reader.GetInt64(2);
                     itemtoadd.IsDeleted = reader.GetInt64(3);
                    ret.Add(itemtoadd);
                }
                return ret;
            }
        }
    }
}
