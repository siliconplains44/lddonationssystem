using System;
using System.Collections.Generic;

using System.Data;

using ldvdbclasslibrary;

namespace ldvdbdal
{
    public class fileuploadsRepository : Repository<fileuploads>, IfileuploadsRepository
    {
        private DbContext _context;
        public fileuploadsRepository(DbContext context)
            : base(context)
        {
            _context = context;
        }
        public long Create(fileuploads newfileuploads)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "INSERT INTO fileuploads (Filename, Size, Created, Data) VALUES (@Filename, @Size, @Created, @Data)";
                var Filenameparam = command.CreateParameter();
                Filenameparam.ParameterName = "@Filename";
                Filenameparam.Value = newfileuploads.Filename;
                command.Parameters.Add(Filenameparam);
                var Sizeparam = command.CreateParameter();
                Sizeparam.ParameterName = "@Size";
                Sizeparam.Value = newfileuploads.Size;
                command.Parameters.Add(Sizeparam);
                var Createdparam = command.CreateParameter();
                Createdparam.ParameterName = "@Created";
                Createdparam.Value = newfileuploads.Created;
                command.Parameters.Add(Createdparam);
                var Dataparam = command.CreateParameter();
                Dataparam.ParameterName = "@Data";
                Dataparam.Value = newfileuploads.Data;
                command.Parameters.Add(Dataparam);
                command.ExecuteNonQuery();
                command.CommandText = "SELECT LAST_INSERT_ID(); ";
                return Convert.ToInt64(command.ExecuteScalar());
            }
        }
        public int Update(fileuploads existingfileuploads)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "UPDATE fileuploads SET Filename = @Filename, Size = @Size, Created = @Created, Data = @Data WHERE FileUploadID = @FileUploadID";
                var FileUploadIDparam = command.CreateParameter();
                FileUploadIDparam.ParameterName = "@FileUploadID";
                FileUploadIDparam.Value = existingfileuploads.FileUploadID;
                command.Parameters.Add(FileUploadIDparam);
                var Filenameparam = command.CreateParameter();
                Filenameparam.ParameterName = "@Filename";
                Filenameparam.Value = existingfileuploads.Filename;
                command.Parameters.Add(Filenameparam);
                var Sizeparam = command.CreateParameter();
                Sizeparam.ParameterName = "@Size";
                Sizeparam.Value = existingfileuploads.Size;
                command.Parameters.Add(Sizeparam);
                var Createdparam = command.CreateParameter();
                Createdparam.ParameterName = "@Created";
                Createdparam.Value = existingfileuploads.Created;
                command.Parameters.Add(Createdparam);
                var Dataparam = command.CreateParameter();
                Dataparam.ParameterName = "@Data";
                Dataparam.Value = existingfileuploads.Data;
                command.Parameters.Add(Dataparam);
                return command.ExecuteNonQuery();
            }
        }
        public int Delete(fileuploads existingfileuploads)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "DELETE FROM fileuploads WHERE FileUploadID = @FileUploadID";
                var FileUploadIDparam = command.CreateParameter();
                FileUploadIDparam.ParameterName = "@FileUploadID";
                FileUploadIDparam.Value = existingfileuploads.FileUploadID;
                command.Parameters.Add(FileUploadIDparam);
                return command.ExecuteNonQuery();
            }
        }
        public fileuploads RetrieveByID(long existingfileuploadsid)
        {
            fileuploads ret = null;
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM fileuploads WHERE FileUploadID = @FileUploadID";
                var FileUploadIDparam = command.CreateParameter();
                FileUploadIDparam.ParameterName = "@FileUploadID";
                FileUploadIDparam.Value = existingfileuploadsid;
                command.Parameters.Add(FileUploadIDparam);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                     ret = new fileuploads();
                     ret.FileUploadID = reader.GetInt64(0);
                     ret.Filename = reader.GetString(1);
                     ret.Size = reader.GetInt64(2);
                     ret.Created = reader.GetDateTime(3);
                     byte[] buffer = new byte[200000000];
                     long read = reader.GetBytes(4, 0, buffer, 0, 2000000000);
                     ret.Data = new byte[read];
                     Buffer.BlockCopy(buffer, 0, ret.Data, 0, Convert.ToInt32(read));
                }
                return ret;
            }
        }
        public List<fileuploads> RetrieveWithWhereClausefileuploads(string WhereClause)
        {
            var ret = new List<fileuploads>();
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM fileuploads WHERE " + WhereClause;
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                     var itemtoadd = new fileuploads();
                     itemtoadd.FileUploadID = reader.GetInt64(0);
                     itemtoadd.Filename = reader.GetString(1);
                     itemtoadd.Size = reader.GetInt64(2);
                     itemtoadd.Created = reader.GetDateTime(3);
                     byte[] buffer = new byte[200000000];
                     long read = reader.GetBytes(4, 0, buffer, 0, 2000000000);
                     itemtoadd.Data = new byte[read];
                     Buffer.BlockCopy(buffer, 0, itemtoadd.Data, 0, Convert.ToInt32(read));
                    ret.Add(itemtoadd);
                }
                return ret;
            }
        }
    }
}
