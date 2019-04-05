using System;
using System.Collections.Generic;

using System.Data;

using ldvdbclasslibrary;

namespace ldvdbdal
{
    public class individualpicturesRepository : Repository<individualpictures>, IindividualpicturesRepository
    {
        private DbContext _context;
        public individualpicturesRepository(DbContext context)
            : base(context)
        {
            _context = context;
        }
        public long Create(individualpictures newindividualpictures)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "INSERT INTO individualpictures (IndividualID, FileUploadID) VALUES (@IndividualID, @FileUploadID)";
                var IndividualIDparam = command.CreateParameter();
                IndividualIDparam.ParameterName = "@IndividualID";
                IndividualIDparam.Value = newindividualpictures.IndividualID;
                command.Parameters.Add(IndividualIDparam);
                var FileUploadIDparam = command.CreateParameter();
                FileUploadIDparam.ParameterName = "@FileUploadID";
                FileUploadIDparam.Value = newindividualpictures.FileUploadID;
                command.Parameters.Add(FileUploadIDparam);
                command.ExecuteNonQuery();
                command.CommandText = "SELECT LAST_INSERT_ID(); ";
                return Convert.ToInt64(command.ExecuteScalar());
            }
        }
        public int Update(individualpictures existingindividualpictures)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "UPDATE individualpictures SET IndividualID = @IndividualID, FileUploadID = @FileUploadID WHERE IndividualPictureID = @IndividualPictureID";
                var IndividualPictureIDparam = command.CreateParameter();
                IndividualPictureIDparam.ParameterName = "@IndividualPictureID";
                IndividualPictureIDparam.Value = existingindividualpictures.IndividualPictureID;
                command.Parameters.Add(IndividualPictureIDparam);
                var IndividualIDparam = command.CreateParameter();
                IndividualIDparam.ParameterName = "@IndividualID";
                IndividualIDparam.Value = existingindividualpictures.IndividualID;
                command.Parameters.Add(IndividualIDparam);
                var FileUploadIDparam = command.CreateParameter();
                FileUploadIDparam.ParameterName = "@FileUploadID";
                FileUploadIDparam.Value = existingindividualpictures.FileUploadID;
                command.Parameters.Add(FileUploadIDparam);
                return command.ExecuteNonQuery();
            }
        }
        public int Delete(individualpictures existingindividualpictures)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "DELETE FROM individualpictures WHERE IndividualPictureID = @IndividualPictureID";
                var IndividualPictureIDparam = command.CreateParameter();
                IndividualPictureIDparam.ParameterName = "@IndividualPictureID";
                IndividualPictureIDparam.Value = existingindividualpictures.IndividualPictureID;
                command.Parameters.Add(IndividualPictureIDparam);
                return command.ExecuteNonQuery();
            }
        }
        public individualpictures RetrieveByID(long existingindividualpicturesid)
        {
            individualpictures ret = null;
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM individualpictures WHERE IndividualPictureID = @IndividualPictureID";
                var IndividualPictureIDparam = command.CreateParameter();
                IndividualPictureIDparam.ParameterName = "@IndividualPictureID";
                IndividualPictureIDparam.Value = existingindividualpicturesid;
                command.Parameters.Add(IndividualPictureIDparam);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                     ret = new individualpictures();
                     ret.IndividualPictureID = reader.GetInt64(0);
                     ret.IndividualID = reader.GetInt64(1);
                     ret.FileUploadID = reader.GetInt64(2);
                }
                return ret;
            }
        }
        public List<individualpictures> RetrieveWithWhereClauseindividualpictures(string WhereClause)
        {
            var ret = new List<individualpictures>();
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM individualpictures WHERE " + WhereClause;
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                     var itemtoadd = new individualpictures();
                     itemtoadd.IndividualPictureID = reader.GetInt64(0);
                     itemtoadd.IndividualID = reader.GetInt64(1);
                     itemtoadd.FileUploadID = reader.GetInt64(2);
                    ret.Add(itemtoadd);
                }
                return ret;
            }
        }
    }
}
