using System;
using System.Collections.Generic;

using System.Data;

using ldvdbclasslibrary;

namespace ldvdbdal
{
    public class happyclientpicturesRepository : Repository<happyclientpictures>, IhappyclientpicturesRepository
    {
        private DbContext _context;
        public happyclientpicturesRepository(DbContext context)
            : base(context)
        {
            _context = context;
        }
        public long Create(happyclientpictures newhappyclientpictures)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "INSERT INTO happyclientpictures (ProgramID, RecipientClientID, FileUploadID) VALUES (@ProgramID, @RecipientClientID, @FileUploadID)";
                var ProgramIDparam = command.CreateParameter();
                ProgramIDparam.ParameterName = "@ProgramID";
                ProgramIDparam.Value = newhappyclientpictures.ProgramID;
                command.Parameters.Add(ProgramIDparam);
                var RecipientClientIDparam = command.CreateParameter();
                RecipientClientIDparam.ParameterName = "@RecipientClientID";
                RecipientClientIDparam.Value = newhappyclientpictures.RecipientClientID;
                command.Parameters.Add(RecipientClientIDparam);
                var FileUploadIDparam = command.CreateParameter();
                FileUploadIDparam.ParameterName = "@FileUploadID";
                FileUploadIDparam.Value = newhappyclientpictures.FileUploadID;
                command.Parameters.Add(FileUploadIDparam);
                command.ExecuteNonQuery();
                command.CommandText = "SELECT LAST_INSERT_ID(); ";
                return Convert.ToInt64(command.ExecuteScalar());
            }
        }
        public int Update(happyclientpictures existinghappyclientpictures)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "UPDATE happyclientpictures SET ProgramID = @ProgramID, RecipientClientID = @RecipientClientID, FileUploadID = @FileUploadID WHERE HappyClientPictureID = @HappyClientPictureID";
                var HappyClientPictureIDparam = command.CreateParameter();
                HappyClientPictureIDparam.ParameterName = "@HappyClientPictureID";
                HappyClientPictureIDparam.Value = existinghappyclientpictures.HappyClientPictureID;
                command.Parameters.Add(HappyClientPictureIDparam);
                var ProgramIDparam = command.CreateParameter();
                ProgramIDparam.ParameterName = "@ProgramID";
                ProgramIDparam.Value = existinghappyclientpictures.ProgramID;
                command.Parameters.Add(ProgramIDparam);
                var RecipientClientIDparam = command.CreateParameter();
                RecipientClientIDparam.ParameterName = "@RecipientClientID";
                RecipientClientIDparam.Value = existinghappyclientpictures.RecipientClientID;
                command.Parameters.Add(RecipientClientIDparam);
                var FileUploadIDparam = command.CreateParameter();
                FileUploadIDparam.ParameterName = "@FileUploadID";
                FileUploadIDparam.Value = existinghappyclientpictures.FileUploadID;
                command.Parameters.Add(FileUploadIDparam);
                return command.ExecuteNonQuery();
            }
        }
        public int Delete(happyclientpictures existinghappyclientpictures)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "DELETE FROM happyclientpictures WHERE HappyClientPictureID = @HappyClientPictureID";
                var HappyClientPictureIDparam = command.CreateParameter();
                HappyClientPictureIDparam.ParameterName = "@HappyClientPictureID";
                HappyClientPictureIDparam.Value = existinghappyclientpictures.HappyClientPictureID;
                command.Parameters.Add(HappyClientPictureIDparam);
                return command.ExecuteNonQuery();
            }
        }
        public happyclientpictures RetrieveByID(long existinghappyclientpicturesid)
        {
            happyclientpictures ret = null;
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM happyclientpictures WHERE HappyClientPictureID = @HappyClientPictureID";
                var HappyClientPictureIDparam = command.CreateParameter();
                HappyClientPictureIDparam.ParameterName = "@HappyClientPictureID";
                HappyClientPictureIDparam.Value = existinghappyclientpicturesid;
                command.Parameters.Add(HappyClientPictureIDparam);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                     ret = new happyclientpictures();
                     ret.HappyClientPictureID = reader.GetInt64(0);
                     ret.ProgramID = reader.GetInt64(1);
                     ret.RecipientClientID = reader.GetInt64(2);
                     ret.FileUploadID = reader.GetInt64(3);
                }
                return ret;
            }
        }
        public List<happyclientpictures> RetrieveWithWhereClausehappyclientpictures(string WhereClause)
        {
            var ret = new List<happyclientpictures>();
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM happyclientpictures WHERE " + WhereClause;
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                     var itemtoadd = new happyclientpictures();
                     itemtoadd.HappyClientPictureID = reader.GetInt64(0);
                     itemtoadd.ProgramID = reader.GetInt64(1);
                     itemtoadd.RecipientClientID = reader.GetInt64(2);
                     itemtoadd.FileUploadID = reader.GetInt64(3);
                    ret.Add(itemtoadd);
                }
                return ret;
            }
        }
    }
}
