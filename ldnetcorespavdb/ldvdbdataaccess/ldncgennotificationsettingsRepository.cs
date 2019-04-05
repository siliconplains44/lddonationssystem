using System;
using System.Collections.Generic;

using System.Data;

using ldvdbclasslibrary;

namespace ldvdbdal
{
    public class notificationsettingsRepository : Repository<notificationsettings>, InotificationsettingsRepository
    {
        private DbContext _context;
        public notificationsettingsRepository(DbContext context)
            : base(context)
        {
            _context = context;
        }
        public long Create(notificationsettings newnotificationsettings)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "INSERT INTO notificationsettings (AccountID, AccountTypeID, EnableEmailMessages, EnableSMSMessages) VALUES (@AccountID, @AccountTypeID, @EnableEmailMessages, @EnableSMSMessages)";
                var AccountIDparam = command.CreateParameter();
                AccountIDparam.ParameterName = "@AccountID";
                AccountIDparam.Value = newnotificationsettings.AccountID;
                command.Parameters.Add(AccountIDparam);
                var AccountTypeIDparam = command.CreateParameter();
                AccountTypeIDparam.ParameterName = "@AccountTypeID";
                AccountTypeIDparam.Value = newnotificationsettings.AccountTypeID;
                command.Parameters.Add(AccountTypeIDparam);
                var EnableEmailMessagesparam = command.CreateParameter();
                EnableEmailMessagesparam.ParameterName = "@EnableEmailMessages";
                EnableEmailMessagesparam.Value = newnotificationsettings.EnableEmailMessages;
                command.Parameters.Add(EnableEmailMessagesparam);
                var EnableSMSMessagesparam = command.CreateParameter();
                EnableSMSMessagesparam.ParameterName = "@EnableSMSMessages";
                EnableSMSMessagesparam.Value = newnotificationsettings.EnableSMSMessages;
                command.Parameters.Add(EnableSMSMessagesparam);
                command.ExecuteNonQuery();
                command.CommandText = "SELECT LAST_INSERT_ID(); ";
                return Convert.ToInt64(command.ExecuteScalar());
            }
        }
        public int Update(notificationsettings existingnotificationsettings)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "UPDATE notificationsettings SET AccountID = @AccountID, AccountTypeID = @AccountTypeID, EnableEmailMessages = @EnableEmailMessages, EnableSMSMessages = @EnableSMSMessages WHERE NotificationSettingID = @NotificationSettingID";
                var NotificationSettingIDparam = command.CreateParameter();
                NotificationSettingIDparam.ParameterName = "@NotificationSettingID";
                NotificationSettingIDparam.Value = existingnotificationsettings.NotificationSettingID;
                command.Parameters.Add(NotificationSettingIDparam);
                var AccountIDparam = command.CreateParameter();
                AccountIDparam.ParameterName = "@AccountID";
                AccountIDparam.Value = existingnotificationsettings.AccountID;
                command.Parameters.Add(AccountIDparam);
                var AccountTypeIDparam = command.CreateParameter();
                AccountTypeIDparam.ParameterName = "@AccountTypeID";
                AccountTypeIDparam.Value = existingnotificationsettings.AccountTypeID;
                command.Parameters.Add(AccountTypeIDparam);
                var EnableEmailMessagesparam = command.CreateParameter();
                EnableEmailMessagesparam.ParameterName = "@EnableEmailMessages";
                EnableEmailMessagesparam.Value = existingnotificationsettings.EnableEmailMessages;
                command.Parameters.Add(EnableEmailMessagesparam);
                var EnableSMSMessagesparam = command.CreateParameter();
                EnableSMSMessagesparam.ParameterName = "@EnableSMSMessages";
                EnableSMSMessagesparam.Value = existingnotificationsettings.EnableSMSMessages;
                command.Parameters.Add(EnableSMSMessagesparam);
                return command.ExecuteNonQuery();
            }
        }
        public int Delete(notificationsettings existingnotificationsettings)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "DELETE FROM notificationsettings WHERE NotificationSettingID = @NotificationSettingID";
                var NotificationSettingIDparam = command.CreateParameter();
                NotificationSettingIDparam.ParameterName = "@NotificationSettingID";
                NotificationSettingIDparam.Value = existingnotificationsettings.NotificationSettingID;
                command.Parameters.Add(NotificationSettingIDparam);
                return command.ExecuteNonQuery();
            }
        }
        public notificationsettings RetrieveByID(long existingnotificationsettingsid)
        {
            notificationsettings ret = null;
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM notificationsettings WHERE NotificationSettingID = @NotificationSettingID";
                var NotificationSettingIDparam = command.CreateParameter();
                NotificationSettingIDparam.ParameterName = "@NotificationSettingID";
                NotificationSettingIDparam.Value = existingnotificationsettingsid;
                command.Parameters.Add(NotificationSettingIDparam);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                     ret = new notificationsettings();
                     ret.NotificationSettingID = reader.GetInt64(0);
                     ret.AccountID = reader.GetInt64(1);
                     ret.AccountTypeID = reader.GetInt64(2);
                     ret.EnableEmailMessages = reader.GetInt64(3);
                     ret.EnableSMSMessages = reader.GetInt64(4);
                }
                return ret;
            }
        }
        public List<notificationsettings> RetrieveWithWhereClausenotificationsettings(string WhereClause)
        {
            var ret = new List<notificationsettings>();
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM notificationsettings WHERE " + WhereClause;
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                     var itemtoadd = new notificationsettings();
                     itemtoadd.NotificationSettingID = reader.GetInt64(0);
                     itemtoadd.AccountID = reader.GetInt64(1);
                     itemtoadd.AccountTypeID = reader.GetInt64(2);
                     itemtoadd.EnableEmailMessages = reader.GetInt64(3);
                     itemtoadd.EnableSMSMessages = reader.GetInt64(4);
                    ret.Add(itemtoadd);
                }
                return ret;
            }
        }
    }
}
