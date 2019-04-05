using System;
using System.Collections.Generic;

using System.Data;

using ldvdbclasslibrary;

namespace ldvdbdal
{
    public class moduleviewsRepository : Repository<moduleviews>, ImoduleviewsRepository
    {
        private DbContext _context;
        public moduleviewsRepository(DbContext context)
            : base(context)
        {
            _context = context;
        }
        public long Create(moduleviews newmoduleviews)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "INSERT INTO moduleviews (Name, Occurred, LoggedInSecurityUserID) VALUES (@Name, @Occurred, @LoggedInSecurityUserID)";
                var Nameparam = command.CreateParameter();
                Nameparam.ParameterName = "@Name";
                Nameparam.Value = newmoduleviews.Name;
                command.Parameters.Add(Nameparam);
                var Occurredparam = command.CreateParameter();
                Occurredparam.ParameterName = "@Occurred";
                Occurredparam.Value = newmoduleviews.Occurred;
                command.Parameters.Add(Occurredparam);
                var LoggedInSecurityUserIDparam = command.CreateParameter();
                LoggedInSecurityUserIDparam.ParameterName = "@LoggedInSecurityUserID";
                LoggedInSecurityUserIDparam.Value = newmoduleviews.LoggedInSecurityUserID;
                command.Parameters.Add(LoggedInSecurityUserIDparam);
                command.ExecuteNonQuery();
                command.CommandText = "SELECT LAST_INSERT_ID(); ";
                return Convert.ToInt64(command.ExecuteScalar());
            }
        }
        public int Update(moduleviews existingmoduleviews)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "UPDATE moduleviews SET Name = @Name, Occurred = @Occurred, LoggedInSecurityUserID = @LoggedInSecurityUserID WHERE ModuleViewID = @ModuleViewID";
                var ModuleViewIDparam = command.CreateParameter();
                ModuleViewIDparam.ParameterName = "@ModuleViewID";
                ModuleViewIDparam.Value = existingmoduleviews.ModuleViewID;
                command.Parameters.Add(ModuleViewIDparam);
                var Nameparam = command.CreateParameter();
                Nameparam.ParameterName = "@Name";
                Nameparam.Value = existingmoduleviews.Name;
                command.Parameters.Add(Nameparam);
                var Occurredparam = command.CreateParameter();
                Occurredparam.ParameterName = "@Occurred";
                Occurredparam.Value = existingmoduleviews.Occurred;
                command.Parameters.Add(Occurredparam);
                var LoggedInSecurityUserIDparam = command.CreateParameter();
                LoggedInSecurityUserIDparam.ParameterName = "@LoggedInSecurityUserID";
                LoggedInSecurityUserIDparam.Value = existingmoduleviews.LoggedInSecurityUserID;
                command.Parameters.Add(LoggedInSecurityUserIDparam);
                return command.ExecuteNonQuery();
            }
        }
        public int Delete(moduleviews existingmoduleviews)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "DELETE FROM moduleviews WHERE ModuleViewID = @ModuleViewID";
                var ModuleViewIDparam = command.CreateParameter();
                ModuleViewIDparam.ParameterName = "@ModuleViewID";
                ModuleViewIDparam.Value = existingmoduleviews.ModuleViewID;
                command.Parameters.Add(ModuleViewIDparam);
                return command.ExecuteNonQuery();
            }
        }
        public moduleviews RetrieveByID(long existingmoduleviewsid)
        {
            moduleviews ret = null;
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM moduleviews WHERE ModuleViewID = @ModuleViewID";
                var ModuleViewIDparam = command.CreateParameter();
                ModuleViewIDparam.ParameterName = "@ModuleViewID";
                ModuleViewIDparam.Value = existingmoduleviewsid;
                command.Parameters.Add(ModuleViewIDparam);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                     ret = new moduleviews();
                     ret.ModuleViewID = reader.GetInt64(0);
                     ret.Name = reader.GetString(1);
                     ret.Occurred = reader.GetDateTime(2);
                     ret.LoggedInSecurityUserID = reader.IsDBNull(3) ? (long?)null : reader.GetInt64(3);
                }
                return ret;
            }
        }
        public List<moduleviews> RetrieveWithWhereClausemoduleviews(string WhereClause)
        {
            var ret = new List<moduleviews>();
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM moduleviews WHERE " + WhereClause;
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                     var itemtoadd = new moduleviews();
                     itemtoadd.ModuleViewID = reader.GetInt64(0);
                     itemtoadd.Name = reader.GetString(1);
                     itemtoadd.Occurred = reader.GetDateTime(2);
                     itemtoadd.LoggedInSecurityUserID = reader.GetInt64(3);
                    ret.Add(itemtoadd);
                }
                return ret;
            }
        }
    }
}
