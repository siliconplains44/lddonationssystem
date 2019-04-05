using System;
using System.Collections.Generic;

using ldvdbclasslibrary;
using ldvdbdal;

namespace ldvdbbusinesslogic
{
    public class messagesManager : ImessagesManager
    {
        private DbContext _dbContext = null;

        private messagesRepository AllocateRepo()
        {
            messagesRepository repo = new messagesRepository(_dbContext);
            return repo;
        }
        public messagesManager(DbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }
        public long Create(messages newmessages)
        {
            return AllocateRepo().Create(newmessages);
        }
        public int Update(messages existingmessages)
        {
            return AllocateRepo().Update(existingmessages);
        }
        public int Delete(messages existingmessages)
        {
            return AllocateRepo().Delete(existingmessages);
        }
        public messages RetrieveByID(long existingmessagesid)
        {
            return AllocateRepo().RetrieveByID(existingmessagesid);
        }
        public List<messages> RetrieveWithWhereClausemessages(string WhereClause)
        {
            return AllocateRepo().RetrieveWithWhereClausemessages(WhereClause);
        }
    }
}
