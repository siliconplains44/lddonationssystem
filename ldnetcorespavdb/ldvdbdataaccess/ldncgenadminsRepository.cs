using System;
using System.Collections.Generic;

using System.Data;

using ldvdbclasslibrary;

namespace ldvdbdal
{
    public class adminsRepository : Repository<admins>, IadminsRepository
    {
        private DbContext _context;
        public adminsRepository(DbContext context)
            : base(context)
        {
            _context = context;
        }
        public long Create(admins newadmins)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "INSERT INTO admins (UserID, IndividualID, IsDeleted) VALUES (@UserID, @IndividualID, @IsDeleted)";
                var UserIDparam = command.CreateParameter();
                UserIDparam.ParameterName = "@UserID";
                UserIDparam.Value = newadmins.UserID;
                command.Parameters.Add(UserIDparam);
                var IndividualIDparam = command.CreateParameter();
                IndividualIDparam.ParameterName = "@IndividualID";
                IndividualIDparam.Value = newadmins.IndividualID;
                command.Parameters.Add(IndividualIDparam);
                var IsDeletedparam = command.CreateParameter();
                IsDeletedparam.ParameterName = "@IsDeleted";
                IsDeletedparam.Value = newadmins.IsDeleted;
                command.Parameters.Add(IsDeletedparam);
                command.ExecuteNonQuery();
                command.CommandText = "SELECT LAST_INSERT_ID(); ";
                return Convert.ToInt64(command.ExecuteScalar());
            }
        }
        public int Update(admins existingadmins)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "UPDATE admins SET UserID = @UserID, IndividualID = @IndividualID, IsDeleted = @IsDeleted WHERE AdminID = @AdminID";
                var AdminIDparam = command.CreateParameter();
                AdminIDparam.ParameterName = "@AdminID";
                AdminIDparam.Value = existingadmins.AdminID;
                command.Parameters.Add(AdminIDparam);
                var UserIDparam = command.CreateParameter();
                UserIDparam.ParameterName = "@UserID";
                UserIDparam.Value = existingadmins.UserID;
                command.Parameters.Add(UserIDparam);
                var IndividualIDparam = command.CreateParameter();
                IndividualIDparam.ParameterName = "@IndividualID";
                IndividualIDparam.Value = existingadmins.IndividualID;
                command.Parameters.Add(IndividualIDparam);
                var IsDeletedparam = command.CreateParameter();
                IsDeletedparam.ParameterName = "@IsDeleted";
                IsDeletedparam.Value = existingadmins.IsDeleted;
                command.Parameters.Add(IsDeletedparam);
                return command.ExecuteNonQuery();
            }
        }
        public int Delete(admins existingadmins)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "DELETE FROM admins WHERE AdminID = @AdminID";
                var AdminIDparam = command.CreateParameter();
                AdminIDparam.ParameterName = "@AdminID";
                AdminIDparam.Value = existingadmins.AdminID;
                command.Parameters.Add(AdminIDparam);
                return command.ExecuteNonQuery();
            }
        }
        public admins RetrieveByID(long existingadminsid)
        {
            admins ret = null;
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM admins WHERE AdminID = @AdminID";
                var AdminIDparam = command.CreateParameter();
                AdminIDparam.ParameterName = "@AdminID";
                AdminIDparam.Value = existingadminsid;
                command.Parameters.Add(AdminIDparam);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                     ret = new admins();
                     ret.AdminID = reader.GetInt64(0);
                     ret.UserID = reader.GetInt64(1);
                     ret.IndividualID = reader.GetInt64(2);
                     ret.IsDeleted = reader.GetInt64(3);
                }
                return ret;
            }
        }
        public List<admins> RetrieveWithWhereClauseadmins(string WhereClause)
        {
            var ret = new List<admins>();
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM admins WHERE " + WhereClause;
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                     var itemtoadd = new admins();
                     itemtoadd.AdminID = reader.GetInt64(0);
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
