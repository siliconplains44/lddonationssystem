using System;
using System.Collections.Generic;

using System.Data;

using ldvdbclasslibrary;

namespace ldvdbdal
{
    public class accounttypesRepository : Repository<accounttypes>, IaccounttypesRepository
    {
        private DbContext _context;
        public accounttypesRepository(DbContext context)
            : base(context)
        {
            _context = context;
        }
        public long Create(accounttypes newaccounttypes)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "INSERT INTO accounttypes (Name) VALUES (@Name)";
                var Nameparam = command.CreateParameter();
                Nameparam.ParameterName = "@Name";
                Nameparam.Value = newaccounttypes.Name;
                command.Parameters.Add(Nameparam);
                command.ExecuteNonQuery();
                command.CommandText = "SELECT LAST_INSERT_ID(); ";
                return Convert.ToInt64(command.ExecuteScalar());
            }
        }
        public int Update(accounttypes existingaccounttypes)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "UPDATE accounttypes SET Name = @Name WHERE AccountTypeID = @AccountTypeID";
                var AccountTypeIDparam = command.CreateParameter();
                AccountTypeIDparam.ParameterName = "@AccountTypeID";
                AccountTypeIDparam.Value = existingaccounttypes.AccountTypeID;
                command.Parameters.Add(AccountTypeIDparam);
                var Nameparam = command.CreateParameter();
                Nameparam.ParameterName = "@Name";
                Nameparam.Value = existingaccounttypes.Name;
                command.Parameters.Add(Nameparam);
                return command.ExecuteNonQuery();
            }
        }
        public int Delete(accounttypes existingaccounttypes)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "DELETE FROM accounttypes WHERE AccountTypeID = @AccountTypeID";
                var AccountTypeIDparam = command.CreateParameter();
                AccountTypeIDparam.ParameterName = "@AccountTypeID";
                AccountTypeIDparam.Value = existingaccounttypes.AccountTypeID;
                command.Parameters.Add(AccountTypeIDparam);
                return command.ExecuteNonQuery();
            }
        }
        public accounttypes RetrieveByID(long existingaccounttypesid)
        {
            accounttypes ret = null;
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM accounttypes WHERE AccountTypeID = @AccountTypeID";
                var AccountTypeIDparam = command.CreateParameter();
                AccountTypeIDparam.ParameterName = "@AccountTypeID";
                AccountTypeIDparam.Value = existingaccounttypesid;
                command.Parameters.Add(AccountTypeIDparam);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                     ret = new accounttypes();
                     ret.AccountTypeID = reader.GetInt64(0);
                     ret.Name = reader.IsDBNull(1) ? null : reader.GetString(1);
                }
                return ret;
            }
        }
        public List<accounttypes> RetrieveWithWhereClauseaccounttypes(string WhereClause)
        {
            var ret = new List<accounttypes>();
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM accounttypes WHERE " + WhereClause;
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                     var itemtoadd = new accounttypes();
                     itemtoadd.AccountTypeID = reader.GetInt64(0);
                     itemtoadd.Name = reader.GetString(1);
                    ret.Add(itemtoadd);
                }
                return ret;
            }
        }
    }
}
