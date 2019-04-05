using System;
using System.Collections.Generic;

using ldvdbclasslibrary;
using ldvdbdal;

namespace ldvdbbusinesslogic
{
    public class messagerecipientsManager : ImessagerecipientsManager
    {
        private DbContext _dbContext = null;

        private messagerecipientsRepository AllocateRepo()
        {
            messagerecipientsRepository repo = new messagerecipientsRepository(_dbContext);
            return repo;
        }
        public messagerecipientsManager(DbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }
        public long Create(messagerecipients newmessagerecipients)
        {
            return AllocateRepo().Create(newmessagerecipients);
        }
        public int Update(messagerecipients existingmessagerecipients)
        {
            return AllocateRepo().Update(existingmessagerecipients);
        }
        public int Delete(messagerecipients existingmessagerecipients)
        {
            return AllocateRepo().Delete(existingmessagerecipients);
        }
        public messagerecipients RetrieveByID(long existingmessagerecipientsid)
        {
            return AllocateRepo().RetrieveByID(existingmessagerecipientsid);
        }
        public List<messagerecipients> RetrieveWithWhereClausemessagerecipients(string WhereClause)
        {
            return AllocateRepo().RetrieveWithWhereClausemessagerecipients(WhereClause);
        }
    }
}
