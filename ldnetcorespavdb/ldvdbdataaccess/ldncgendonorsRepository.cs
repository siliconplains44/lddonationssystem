using System;
using System.Collections.Generic;

using System.Data;

using ldvdbclasslibrary;

namespace ldvdbdal
{
    public class donorsRepository : Repository<donors>, IdonorsRepository
    {
        private DbContext _context;
        public donorsRepository(DbContext context)
            : base(context)
        {
            _context = context;
        }
        public long Create(donors newdonors)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "INSERT INTO donors (UserID, IndividualID, IsDeleted) VALUES (@UserID, @IndividualID, @IsDeleted)";
                var UserIDparam = command.CreateParameter();
                UserIDparam.ParameterName = "@UserID";
                UserIDparam.Value = newdonors.UserID;
                command.Parameters.Add(UserIDparam);
                var IndividualIDparam = command.CreateParameter();
                IndividualIDparam.ParameterName = "@IndividualID";
                IndividualIDparam.Value = newdonors.IndividualID;
                command.Parameters.Add(IndividualIDparam);
                var IsDeletedparam = command.CreateParameter();
                IsDeletedparam.ParameterName = "@IsDeleted";
                IsDeletedparam.Value = newdonors.IsDeleted;
                command.Parameters.Add(IsDeletedparam);
                command.ExecuteNonQuery();
                command.CommandText = "SELECT LAST_INSERT_ID(); ";
                return Convert.ToInt64(command.ExecuteScalar());
            }
        }
        public int Update(donors existingdonors)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "UPDATE donors SET UserID = @UserID, IndividualID = @IndividualID, IsDeleted = @IsDeleted WHERE DonorID = @DonorID";
                var DonorIDparam = command.CreateParameter();
                DonorIDparam.ParameterName = "@DonorID";
                DonorIDparam.Value = existingdonors.DonorID;
                command.Parameters.Add(DonorIDparam);
                var UserIDparam = command.CreateParameter();
                UserIDparam.ParameterName = "@UserID";
                UserIDparam.Value = existingdonors.UserID;
                command.Parameters.Add(UserIDparam);
                var IndividualIDparam = command.CreateParameter();
                IndividualIDparam.ParameterName = "@IndividualID";
                IndividualIDparam.Value = existingdonors.IndividualID;
                command.Parameters.Add(IndividualIDparam);
                var IsDeletedparam = command.CreateParameter();
                IsDeletedparam.ParameterName = "@IsDeleted";
                IsDeletedparam.Value = existingdonors.IsDeleted;
                command.Parameters.Add(IsDeletedparam);
                return command.ExecuteNonQuery();
            }
        }
        public int Delete(donors existingdonors)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "DELETE FROM donors WHERE DonorID = @DonorID";
                var DonorIDparam = command.CreateParameter();
                DonorIDparam.ParameterName = "@DonorID";
                DonorIDparam.Value = existingdonors.DonorID;
                command.Parameters.Add(DonorIDparam);
                return command.ExecuteNonQuery();
            }
        }
        public donors RetrieveByID(long existingdonorsid)
        {
            donors ret = null;
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM donors WHERE DonorID = @DonorID";
                var DonorIDparam = command.CreateParameter();
                DonorIDparam.ParameterName = "@DonorID";
                DonorIDparam.Value = existingdonorsid;
                command.Parameters.Add(DonorIDparam);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                     ret = new donors();
                     ret.DonorID = reader.GetInt64(0);
                     ret.UserID = reader.GetInt64(1);
                     ret.IndividualID = reader.GetInt64(2);
                     ret.IsDeleted = reader.GetInt64(3);
                }
                return ret;
            }
        }
        public List<donors> RetrieveWithWhereClausedonors(string WhereClause)
        {
            var ret = new List<donors>();
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM donors WHERE " + WhereClause;
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                     var itemtoadd = new donors();
                     itemtoadd.DonorID = reader.GetInt64(0);
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
