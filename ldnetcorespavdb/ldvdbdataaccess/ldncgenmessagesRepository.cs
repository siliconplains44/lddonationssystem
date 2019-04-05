using System;
using System.Collections.Generic;

using System.Data;

using ldvdbclasslibrary;

namespace ldvdbdal
{
    public class messagesRepository : Repository<messages>, ImessagesRepository
    {
        private DbContext _context;
        public messagesRepository(DbContext context)
            : base(context)
        {
            _context = context;
        }
        public long Create(messages newmessages)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "INSERT INTO messages (FromAccountID, FromAccountTypeID, MessageSentDateTime, Subject, Body) VALUES (@FromAccountID, @FromAccountTypeID, @MessageSentDateTime, @Subject, @Body)";
                var FromAccountIDparam = command.CreateParameter();
                FromAccountIDparam.ParameterName = "@FromAccountID";
                FromAccountIDparam.Value = newmessages.FromAccountID;
                command.Parameters.Add(FromAccountIDparam);
                var FromAccountTypeIDparam = command.CreateParameter();
                FromAccountTypeIDparam.ParameterName = "@FromAccountTypeID";
                FromAccountTypeIDparam.Value = newmessages.FromAccountTypeID;
                command.Parameters.Add(FromAccountTypeIDparam);
                var MessageSentDateTimeparam = command.CreateParameter();
                MessageSentDateTimeparam.ParameterName = "@MessageSentDateTime";
                MessageSentDateTimeparam.Value = newmessages.MessageSentDateTime;
                command.Parameters.Add(MessageSentDateTimeparam);
                var Subjectparam = command.CreateParameter();
                Subjectparam.ParameterName = "@Subject";
                Subjectparam.Value = newmessages.Subject;
                command.Parameters.Add(Subjectparam);
                var Bodyparam = command.CreateParameter();
                Bodyparam.ParameterName = "@Body";
                Bodyparam.Value = newmessages.Body;
                command.Parameters.Add(Bodyparam);
                command.ExecuteNonQuery();
                command.CommandText = "SELECT LAST_INSERT_ID(); ";
                return Convert.ToInt64(command.ExecuteScalar());
            }
        }
        public int Update(messages existingmessages)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "UPDATE messages SET FromAccountID = @FromAccountID, FromAccountTypeID = @FromAccountTypeID, MessageSentDateTime = @MessageSentDateTime, Subject = @Subject, Body = @Body WHERE MessageID = @MessageID";
                var MessageIDparam = command.CreateParameter();
                MessageIDparam.ParameterName = "@MessageID";
                MessageIDparam.Value = existingmessages.MessageID;
                command.Parameters.Add(MessageIDparam);
                var FromAccountIDparam = command.CreateParameter();
                FromAccountIDparam.ParameterName = "@FromAccountID";
                FromAccountIDparam.Value = existingmessages.FromAccountID;
                command.Parameters.Add(FromAccountIDparam);
                var FromAccountTypeIDparam = command.CreateParameter();
                FromAccountTypeIDparam.ParameterName = "@FromAccountTypeID";
                FromAccountTypeIDparam.Value = existingmessages.FromAccountTypeID;
                command.Parameters.Add(FromAccountTypeIDparam);
                var MessageSentDateTimeparam = command.CreateParameter();
                MessageSentDateTimeparam.ParameterName = "@MessageSentDateTime";
                MessageSentDateTimeparam.Value = existingmessages.MessageSentDateTime;
                command.Parameters.Add(MessageSentDateTimeparam);
                var Subjectparam = command.CreateParameter();
                Subjectparam.ParameterName = "@Subject";
                Subjectparam.Value = existingmessages.Subject;
                command.Parameters.Add(Subjectparam);
                var Bodyparam = command.CreateParameter();
                Bodyparam.ParameterName = "@Body";
                Bodyparam.Value = existingmessages.Body;
                command.Parameters.Add(Bodyparam);
                return command.ExecuteNonQuery();
            }
        }
        public int Delete(messages existingmessages)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "DELETE FROM messages WHERE MessageID = @MessageID";
                var MessageIDparam = command.CreateParameter();
                MessageIDparam.ParameterName = "@MessageID";
                MessageIDparam.Value = existingmessages.MessageID;
                command.Parameters.Add(MessageIDparam);
                return command.ExecuteNonQuery();
            }
        }
        public messages RetrieveByID(long existingmessagesid)
        {
            messages ret = null;
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM messages WHERE MessageID = @MessageID";
                var MessageIDparam = command.CreateParameter();
                MessageIDparam.ParameterName = "@MessageID";
                MessageIDparam.Value = existingmessagesid;
                command.Parameters.Add(MessageIDparam);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                     ret = new messages();
                     ret.MessageID = reader.GetInt64(0);
                     ret.FromAccountID = reader.GetInt64(1);
                     ret.FromAccountTypeID = reader.GetInt64(2);
                     ret.MessageSentDateTime = reader.GetDateTime(3);
                     ret.Subject = reader.GetString(4);
                     ret.Body = reader.GetString(5);
                }
                return ret;
            }
        }
        public List<messages> RetrieveWithWhereClausemessages(string WhereClause)
        {
            var ret = new List<messages>();
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM messages WHERE " + WhereClause;
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                     var itemtoadd = new messages();
                     itemtoadd.MessageID = reader.GetInt64(0);
                     itemtoadd.FromAccountID = reader.GetInt64(1);
                     itemtoadd.FromAccountTypeID = reader.GetInt64(2);
                     itemtoadd.MessageSentDateTime = reader.GetDateTime(3);
                     itemtoadd.Subject = reader.GetString(4);
                     itemtoadd.Body = reader.GetString(5);
                    ret.Add(itemtoadd);
                }
                return ret;
            }
        }
    }
}
