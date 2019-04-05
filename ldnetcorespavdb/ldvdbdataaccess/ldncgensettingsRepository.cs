using System;
using System.Collections.Generic;

using System.Data;

using ldvdbclasslibrary;

namespace ldvdbdal
{
    public class settingsRepository : Repository<settings>, IsettingsRepository
    {
        private DbContext _context;
        public settingsRepository(DbContext context)
            : base(context)
        {
            _context = context;
        }
        public long Create(settings newsettings)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "INSERT INTO settings (Name, Value) VALUES (@Name, @Value)";
                var Nameparam = command.CreateParameter();
                Nameparam.ParameterName = "@Name";
                Nameparam.Value = newsettings.Name;
                command.Parameters.Add(Nameparam);
                var Valueparam = command.CreateParameter();
                Valueparam.ParameterName = "@Value";
                Valueparam.Value = newsettings.Value;
                command.Parameters.Add(Valueparam);
                command.ExecuteNonQuery();
                command.CommandText = "SELECT LAST_INSERT_ID(); ";
                return Convert.ToInt64(command.ExecuteScalar());
            }
        }
        public int Update(settings existingsettings)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "UPDATE settings SET Name = @Name, Value = @Value WHERE SettingID = @SettingID";
                var SettingIDparam = command.CreateParameter();
                SettingIDparam.ParameterName = "@SettingID";
                SettingIDparam.Value = existingsettings.SettingID;
                command.Parameters.Add(SettingIDparam);
                var Nameparam = command.CreateParameter();
                Nameparam.ParameterName = "@Name";
                Nameparam.Value = existingsettings.Name;
                command.Parameters.Add(Nameparam);
                var Valueparam = command.CreateParameter();
                Valueparam.ParameterName = "@Value";
                Valueparam.Value = existingsettings.Value;
                command.Parameters.Add(Valueparam);
                return command.ExecuteNonQuery();
            }
        }
        public int Delete(settings existingsettings)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "DELETE FROM settings WHERE SettingID = @SettingID";
                var SettingIDparam = command.CreateParameter();
                SettingIDparam.ParameterName = "@SettingID";
                SettingIDparam.Value = existingsettings.SettingID;
                command.Parameters.Add(SettingIDparam);
                return command.ExecuteNonQuery();
            }
        }
        public settings RetrieveByID(long existingsettingsid)
        {
            settings ret = null;
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM settings WHERE SettingID = @SettingID";
                var SettingIDparam = command.CreateParameter();
                SettingIDparam.ParameterName = "@SettingID";
                SettingIDparam.Value = existingsettingsid;
                command.Parameters.Add(SettingIDparam);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                     ret = new settings();
                     ret.SettingID = reader.GetInt64(0);
                     ret.Name = reader.IsDBNull(1) ? null : reader.GetString(1);
                     ret.Value = reader.IsDBNull(2) ? null : reader.GetString(2);
                }
                return ret;
            }
        }
        public List<settings> RetrieveWithWhereClausesettings(string WhereClause)
        {
            var ret = new List<settings>();
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM settings WHERE " + WhereClause;
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                     var itemtoadd = new settings();
                     itemtoadd.SettingID = reader.GetInt64(0);
                     itemtoadd.Name = reader.GetString(1);
                     itemtoadd.Value = reader.GetString(2);
                    ret.Add(itemtoadd);
                }
                return ret;
            }
        }
    }
}
