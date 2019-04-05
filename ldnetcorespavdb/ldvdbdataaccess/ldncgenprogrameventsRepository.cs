using System;
using System.Collections.Generic;

using System.Data;

using ldvdbclasslibrary;

namespace ldvdbdal
{
    public class programeventsRepository : Repository<programevents>, IprogrameventsRepository
    {
        private DbContext _context;
        public programeventsRepository(DbContext context)
            : base(context)
        {
            _context = context;
        }
        public long Create(programevents newprogramevents)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "INSERT INTO programevents (ProgramID, IsSingleDate, FromDate, ToDate, Description, Name) VALUES (@ProgramID, @IsSingleDate, @FromDate, @ToDate, @Description, @Name)";
                var ProgramIDparam = command.CreateParameter();
                ProgramIDparam.ParameterName = "@ProgramID";
                ProgramIDparam.Value = newprogramevents.ProgramID;
                command.Parameters.Add(ProgramIDparam);
                var IsSingleDateparam = command.CreateParameter();
                IsSingleDateparam.ParameterName = "@IsSingleDate";
                IsSingleDateparam.Value = newprogramevents.IsSingleDate;
                command.Parameters.Add(IsSingleDateparam);
                var FromDateparam = command.CreateParameter();
                FromDateparam.ParameterName = "@FromDate";
                FromDateparam.Value = newprogramevents.FromDate;
                command.Parameters.Add(FromDateparam);
                var ToDateparam = command.CreateParameter();
                ToDateparam.ParameterName = "@ToDate";
                ToDateparam.Value = newprogramevents.ToDate;
                command.Parameters.Add(ToDateparam);
                var Descriptionparam = command.CreateParameter();
                Descriptionparam.ParameterName = "@Description";
                Descriptionparam.Value = newprogramevents.Description;
                command.Parameters.Add(Descriptionparam);
                var Nameparam = command.CreateParameter();
                Nameparam.ParameterName = "@Name";
                Nameparam.Value = newprogramevents.Name;
                command.Parameters.Add(Nameparam);
                command.ExecuteNonQuery();
                command.CommandText = "SELECT LAST_INSERT_ID(); ";
                return Convert.ToInt64(command.ExecuteScalar());
            }
        }
        public int Update(programevents existingprogramevents)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "UPDATE programevents SET ProgramID = @ProgramID, IsSingleDate = @IsSingleDate, FromDate = @FromDate, ToDate = @ToDate, Description = @Description, Name = @Name WHERE ProgramEventID = @ProgramEventID";
                var ProgramEventIDparam = command.CreateParameter();
                ProgramEventIDparam.ParameterName = "@ProgramEventID";
                ProgramEventIDparam.Value = existingprogramevents.ProgramEventID;
                command.Parameters.Add(ProgramEventIDparam);
                var ProgramIDparam = command.CreateParameter();
                ProgramIDparam.ParameterName = "@ProgramID";
                ProgramIDparam.Value = existingprogramevents.ProgramID;
                command.Parameters.Add(ProgramIDparam);
                var IsSingleDateparam = command.CreateParameter();
                IsSingleDateparam.ParameterName = "@IsSingleDate";
                IsSingleDateparam.Value = existingprogramevents.IsSingleDate;
                command.Parameters.Add(IsSingleDateparam);
                var FromDateparam = command.CreateParameter();
                FromDateparam.ParameterName = "@FromDate";
                FromDateparam.Value = existingprogramevents.FromDate;
                command.Parameters.Add(FromDateparam);
                var ToDateparam = command.CreateParameter();
                ToDateparam.ParameterName = "@ToDate";
                ToDateparam.Value = existingprogramevents.ToDate;
                command.Parameters.Add(ToDateparam);
                var Descriptionparam = command.CreateParameter();
                Descriptionparam.ParameterName = "@Description";
                Descriptionparam.Value = existingprogramevents.Description;
                command.Parameters.Add(Descriptionparam);
                var Nameparam = command.CreateParameter();
                Nameparam.ParameterName = "@Name";
                Nameparam.Value = existingprogramevents.Name;
                command.Parameters.Add(Nameparam);
                return command.ExecuteNonQuery();
            }
        }
        public int Delete(programevents existingprogramevents)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "DELETE FROM programevents WHERE ProgramEventID = @ProgramEventID";
                var ProgramEventIDparam = command.CreateParameter();
                ProgramEventIDparam.ParameterName = "@ProgramEventID";
                ProgramEventIDparam.Value = existingprogramevents.ProgramEventID;
                command.Parameters.Add(ProgramEventIDparam);
                return command.ExecuteNonQuery();
            }
        }
        public programevents RetrieveByID(long existingprogrameventsid)
        {
            programevents ret = null;
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM programevents WHERE ProgramEventID = @ProgramEventID";
                var ProgramEventIDparam = command.CreateParameter();
                ProgramEventIDparam.ParameterName = "@ProgramEventID";
                ProgramEventIDparam.Value = existingprogrameventsid;
                command.Parameters.Add(ProgramEventIDparam);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                     ret = new programevents();
                     ret.ProgramEventID = reader.GetInt64(0);
                     ret.ProgramID = reader.GetInt64(1);
                     ret.IsSingleDate = reader.GetInt64(2);
                     ret.FromDate = reader.GetDateTime(3);
                     ret.ToDate = reader.GetDateTime(4);
                     ret.Description = reader.GetString(5);
                     ret.Name = reader.IsDBNull(6) ? null : reader.GetString(6);
                }
                return ret;
            }
        }
        public List<programevents> RetrieveWithWhereClauseprogramevents(string WhereClause)
        {
            var ret = new List<programevents>();
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM programevents WHERE " + WhereClause;
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                     var itemtoadd = new programevents();
                     itemtoadd.ProgramEventID = reader.GetInt64(0);
                     itemtoadd.ProgramID = reader.GetInt64(1);
                     itemtoadd.IsSingleDate = reader.GetInt64(2);
                     itemtoadd.FromDate = reader.GetDateTime(3);
                     itemtoadd.ToDate = reader.GetDateTime(4);
                     itemtoadd.Description = reader.GetString(5);
                     itemtoadd.Name = reader.GetString(6);
                    ret.Add(itemtoadd);
                }
                return ret;
            }
        }
    }
}
