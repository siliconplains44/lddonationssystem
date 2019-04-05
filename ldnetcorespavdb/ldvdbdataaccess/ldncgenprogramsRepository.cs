using System;
using System.Collections.Generic;

using System.Data;

using ldvdbclasslibrary;

namespace ldvdbdal
{
    public class programsRepository : Repository<programs>, IprogramsRepository
    {
        private DbContext _context;
        public programsRepository(DbContext context)
            : base(context)
        {
            _context = context;
        }
        public long Create(programs newprograms)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "INSERT INTO programs (Name, Description, IsPublished, Year) VALUES (@Name, @Description, @IsPublished, @Year)";
                var Nameparam = command.CreateParameter();
                Nameparam.ParameterName = "@Name";
                Nameparam.Value = newprograms.Name;
                command.Parameters.Add(Nameparam);
                var Descriptionparam = command.CreateParameter();
                Descriptionparam.ParameterName = "@Description";
                Descriptionparam.Value = newprograms.Description;
                command.Parameters.Add(Descriptionparam);
                var IsPublishedparam = command.CreateParameter();
                IsPublishedparam.ParameterName = "@IsPublished";
                IsPublishedparam.Value = newprograms.IsPublished;
                command.Parameters.Add(IsPublishedparam);
                var Yearparam = command.CreateParameter();
                Yearparam.ParameterName = "@Year";
                Yearparam.Value = newprograms.Year;
                command.Parameters.Add(Yearparam);
                command.ExecuteNonQuery();
                command.CommandText = "SELECT LAST_INSERT_ID(); ";
                return Convert.ToInt64(command.ExecuteScalar());
            }
        }
        public int Update(programs existingprograms)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "UPDATE programs SET Name = @Name, Description = @Description, IsPublished = @IsPublished, Year = @Year WHERE ProgramID = @ProgramID";
                var ProgramIDparam = command.CreateParameter();
                ProgramIDparam.ParameterName = "@ProgramID";
                ProgramIDparam.Value = existingprograms.ProgramID;
                command.Parameters.Add(ProgramIDparam);
                var Nameparam = command.CreateParameter();
                Nameparam.ParameterName = "@Name";
                Nameparam.Value = existingprograms.Name;
                command.Parameters.Add(Nameparam);
                var Descriptionparam = command.CreateParameter();
                Descriptionparam.ParameterName = "@Description";
                Descriptionparam.Value = existingprograms.Description;
                command.Parameters.Add(Descriptionparam);
                var IsPublishedparam = command.CreateParameter();
                IsPublishedparam.ParameterName = "@IsPublished";
                IsPublishedparam.Value = existingprograms.IsPublished;
                command.Parameters.Add(IsPublishedparam);
                var Yearparam = command.CreateParameter();
                Yearparam.ParameterName = "@Year";
                Yearparam.Value = existingprograms.Year;
                command.Parameters.Add(Yearparam);
                return command.ExecuteNonQuery();
            }
        }
        public int Delete(programs existingprograms)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "DELETE FROM programs WHERE ProgramID = @ProgramID";
                var ProgramIDparam = command.CreateParameter();
                ProgramIDparam.ParameterName = "@ProgramID";
                ProgramIDparam.Value = existingprograms.ProgramID;
                command.Parameters.Add(ProgramIDparam);
                return command.ExecuteNonQuery();
            }
        }
        public programs RetrieveByID(long existingprogramsid)
        {
            programs ret = null;
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM programs WHERE ProgramID = @ProgramID";
                var ProgramIDparam = command.CreateParameter();
                ProgramIDparam.ParameterName = "@ProgramID";
                ProgramIDparam.Value = existingprogramsid;
                command.Parameters.Add(ProgramIDparam);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                     ret = new programs();
                     ret.ProgramID = reader.GetInt64(0);
                     ret.Name = reader.GetString(1);
                     ret.Description = reader.GetString(2);
                     ret.IsPublished = reader.GetInt64(3);
                     ret.Year = reader.GetInt64(4);
                }
                return ret;
            }
        }
        public List<programs> RetrieveWithWhereClauseprograms(string WhereClause)
        {
            var ret = new List<programs>();
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM programs WHERE " + WhereClause;
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                     var itemtoadd = new programs();
                     itemtoadd.ProgramID = reader.GetInt64(0);
                     itemtoadd.Name = reader.GetString(1);
                     itemtoadd.Description = reader.GetString(2);
                     itemtoadd.IsPublished = reader.GetInt64(3);
                     itemtoadd.Year = reader.GetInt64(4);
                    ret.Add(itemtoadd);
                }
                return ret;
            }
        }
    }
}
