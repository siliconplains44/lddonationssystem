using System;
using System.Collections.Generic;

using System.Data;

using ldvdbclasslibrary;

namespace ldvdbdal
{
    public class messagerecipientsRepository : Repository<messagerecipients>, ImessagerecipientsRepository
    {
        private DbContext _context;
        public messagerecipientsRepository(DbContext context)
            : base(context)
        {
            _context = context;
        }
        public long Create(messagerecipients newmessagerecipients)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "INSERT INTO messagerecipients (MessageID, AccountID, AccountTypeID, MessageRead) VALUES (@MessageID, @AccountID, @AccountTypeID, @MessageRead)";
                var MessageIDparam = command.CreateParameter();
                MessageIDparam.ParameterName = "@MessageID";
                MessageIDparam.Value = newmessagerecipients.MessageID;
                command.Parameters.Add(MessageIDparam);
                var AccountIDparam = command.CreateParameter();
                AccountIDparam.ParameterName = "@AccountID";
                AccountIDparam.Value = newmessagerecipients.AccountID;
                command.Parameters.Add(AccountIDparam);
                var AccountTypeIDparam = command.CreateParameter();
                AccountTypeIDparam.ParameterName = "@AccountTypeID";
                AccountTypeIDparam.Value = newmessagerecipients.AccountTypeID;
                command.Parameters.Add(AccountTypeIDparam);
                var MessageReadparam = command.CreateParameter();
                MessageReadparam.ParameterName = "@MessageRead";
                MessageReadparam.Value = newmessagerecipients.MessageRead;
                command.Parameters.Add(MessageReadparam);
                command.ExecuteNonQuery();
                command.CommandText = "SELECT LAST_INSERT_ID(); ";
                return Convert.ToInt64(command.ExecuteScalar());
            }
        }
        public int Update(messagerecipients existingmessagerecipients)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "UPDATE messagerecipients SET MessageID = @MessageID, AccountID = @AccountID, AccountTypeID = @AccountTypeID, MessageRead = @MessageRead WHERE MessageRecipientID = @MessageRecipientID";
                var MessageRecipientIDparam = command.CreateParameter();
                MessageRecipientIDparam.ParameterName = "@MessageRecipientID";
                MessageRecipientIDparam.Value = existingmessagerecipients.MessageRecipientID;
                command.Parameters.Add(MessageRecipientIDparam);
                var MessageIDparam = command.CreateParameter();
                MessageIDparam.ParameterName = "@MessageID";
                MessageIDparam.Value = existingmessagerecipients.MessageID;
                command.Parameters.Add(MessageIDparam);
                var AccountIDparam = command.CreateParameter();
                AccountIDparam.ParameterName = "@AccountID";
                AccountIDparam.Value = existingmessagerecipients.AccountID;
                command.Parameters.Add(AccountIDparam);
                var AccountTypeIDparam = command.CreateParameter();
                AccountTypeIDparam.ParameterName = "@AccountTypeID";
                AccountTypeIDparam.Value = existingmessagerecipients.AccountTypeID;
                command.Parameters.Add(AccountTypeIDparam);
                var MessageReadparam = command.CreateParameter();
                MessageReadparam.ParameterName = "@MessageRead";
                MessageReadparam.Value = existingmessagerecipients.MessageRead;
                command.Parameters.Add(MessageReadparam);
                return command.ExecuteNonQuery();
            }
        }
        public int Delete(messagerecipients existingmessagerecipients)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "DELETE FROM messagerecipients WHERE MessageRecipientID = @MessageRecipientID";
                var MessageRecipientIDparam = command.CreateParameter();
                MessageRecipientIDparam.ParameterName = "@MessageRecipientID";
                MessageRecipientIDparam.Value = existingmessagerecipients.MessageRecipientID;
                command.Parameters.Add(MessageRecipientIDparam);
                return command.ExecuteNonQuery();
            }
        }
        public messagerecipients RetrieveByID(long existingmessagerecipientsid)
        {
            messagerecipients ret = null;
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM messagerecipients WHERE MessageRecipientID = @MessageRecipientID";
                var MessageRecipientIDparam = command.CreateParameter();
                MessageRecipientIDparam.ParameterName = "@MessageRecipientID";
                MessageRecipientIDparam.Value = existingmessagerecipientsid;
                command.Parameters.Add(MessageRecipientIDparam);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                     ret = new messagerecipients();
                     ret.MessageRecipientID = reader.GetInt64(0);
                     ret.MessageID = reader.GetInt64(1);
                     ret.AccountID = reader.GetInt64(2);
                     ret.AccountTypeID = reader.GetInt64(3);
                     ret.MessageRead = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4);
                }
                return ret;
            }
        }
        public List<messagerecipients> RetrieveWithWhereClausemessagerecipients(string WhereClause)
        {
            var ret = new List<messagerecipients>();
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM messagerecipients WHERE " + WhereClause;
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                     var itemtoadd = new messagerecipients();
                     itemtoadd.MessageRecipientID = reader.GetInt64(0);
                     itemtoadd.MessageID = reader.GetInt64(1);
                     itemtoadd.AccountID = reader.GetInt64(2);
                     itemtoadd.AccountTypeID = reader.GetInt64(3);
                     itemtoadd.MessageRead = reader.GetDateTime(4);
                    ret.Add(itemtoadd);
                }
                return ret;
            }
        }
    }
}
