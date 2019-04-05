using System;
using System.Collections.Generic;

using System.Data;

using ldvdbclasslibrary;

namespace ldvdbdal
{
    public class usersRepository : Repository<users>, IusersRepository
    {
        private DbContext _context;
        public usersRepository(DbContext context)
            : base(context)
        {
            _context = context;
        }
        public long Create(users newusers)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "INSERT INTO users (Email, Password, IsActive, RegistrationCode, PasswordResetCode, AccountTypeID) VALUES (@Email, @Password, @IsActive, @RegistrationCode, @PasswordResetCode, @AccountTypeID)";
                var Emailparam = command.CreateParameter();
                Emailparam.ParameterName = "@Email";
                Emailparam.Value = newusers.Email;
                command.Parameters.Add(Emailparam);
                var Passwordparam = command.CreateParameter();
                Passwordparam.ParameterName = "@Password";
                Passwordparam.Value = newusers.Password;
                command.Parameters.Add(Passwordparam);
                var IsActiveparam = command.CreateParameter();
                IsActiveparam.ParameterName = "@IsActive";
                IsActiveparam.Value = newusers.IsActive;
                command.Parameters.Add(IsActiveparam);
                var RegistrationCodeparam = command.CreateParameter();
                RegistrationCodeparam.ParameterName = "@RegistrationCode";
                RegistrationCodeparam.Value = newusers.RegistrationCode;
                command.Parameters.Add(RegistrationCodeparam);
                var PasswordResetCodeparam = command.CreateParameter();
                PasswordResetCodeparam.ParameterName = "@PasswordResetCode";
                PasswordResetCodeparam.Value = newusers.PasswordResetCode;
                command.Parameters.Add(PasswordResetCodeparam);
                var AccountTypeIDparam = command.CreateParameter();
                AccountTypeIDparam.ParameterName = "@AccountTypeID";
                AccountTypeIDparam.Value = newusers.AccountTypeID;
                command.Parameters.Add(AccountTypeIDparam);
                command.ExecuteNonQuery();
                command.CommandText = "SELECT LAST_INSERT_ID(); ";
                return Convert.ToInt64(command.ExecuteScalar());
            }
        }
        public int Update(users existingusers)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "UPDATE users SET Email = @Email, Password = @Password, IsActive = @IsActive, RegistrationCode = @RegistrationCode, PasswordResetCode = @PasswordResetCode, AccountTypeID = @AccountTypeID WHERE UserID = @UserID";
                var UserIDparam = command.CreateParameter();
                UserIDparam.ParameterName = "@UserID";
                UserIDparam.Value = existingusers.UserID;
                command.Parameters.Add(UserIDparam);
                var Emailparam = command.CreateParameter();
                Emailparam.ParameterName = "@Email";
                Emailparam.Value = existingusers.Email;
                command.Parameters.Add(Emailparam);
                var Passwordparam = command.CreateParameter();
                Passwordparam.ParameterName = "@Password";
                Passwordparam.Value = existingusers.Password;
                command.Parameters.Add(Passwordparam);
                var IsActiveparam = command.CreateParameter();
                IsActiveparam.ParameterName = "@IsActive";
                IsActiveparam.Value = existingusers.IsActive;
                command.Parameters.Add(IsActiveparam);
                var RegistrationCodeparam = command.CreateParameter();
                RegistrationCodeparam.ParameterName = "@RegistrationCode";
                RegistrationCodeparam.Value = existingusers.RegistrationCode;
                command.Parameters.Add(RegistrationCodeparam);
                var PasswordResetCodeparam = command.CreateParameter();
                PasswordResetCodeparam.ParameterName = "@PasswordResetCode";
                PasswordResetCodeparam.Value = existingusers.PasswordResetCode;
                command.Parameters.Add(PasswordResetCodeparam);
                var AccountTypeIDparam = command.CreateParameter();
                AccountTypeIDparam.ParameterName = "@AccountTypeID";
                AccountTypeIDparam.Value = existingusers.AccountTypeID;
                command.Parameters.Add(AccountTypeIDparam);
                return command.ExecuteNonQuery();
            }
        }
        public int Delete(users existingusers)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "DELETE FROM users WHERE UserID = @UserID";
                var UserIDparam = command.CreateParameter();
                UserIDparam.ParameterName = "@UserID";
                UserIDparam.Value = existingusers.UserID;
                command.Parameters.Add(UserIDparam);
                return command.ExecuteNonQuery();
            }
        }
        public users RetrieveByID(long existingusersid)
        {
            users ret = null;
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM users WHERE UserID = @UserID";
                var UserIDparam = command.CreateParameter();
                UserIDparam.ParameterName = "@UserID";
                UserIDparam.Value = existingusersid;
                command.Parameters.Add(UserIDparam);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                     ret = new users();
                     ret.UserID = reader.GetInt64(0);
                     ret.Email = reader.GetString(1);
                     ret.Password = reader.IsDBNull(2) ? null : reader.GetString(2);
                     ret.IsActive = reader.GetInt64(3);
                     ret.RegistrationCode = reader.IsDBNull(4) ? null : reader.GetString(4);
                     ret.PasswordResetCode = reader.IsDBNull(5) ? null : reader.GetString(5);
                     ret.AccountTypeID = reader.GetInt64(6);
                }
                return ret;
            }
        }
        public List<users> RetrieveWithWhereClauseusers(string WhereClause)
        {
            var ret = new List<users>();
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM users WHERE " + WhereClause;
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                     var itemtoadd = new users();
                     itemtoadd.UserID = reader.GetInt64(0);
                     itemtoadd.Email = reader.GetString(1);
                     itemtoadd.Password = reader.GetString(2);
                     itemtoadd.IsActive = reader.GetInt64(3);
                     itemtoadd.RegistrationCode = reader.GetString(4);
                     itemtoadd.PasswordResetCode = reader.GetString(5);
                     itemtoadd.AccountTypeID = reader.GetInt64(6);
                    ret.Add(itemtoadd);
                }
                return ret;
            }
        }
    }
}
