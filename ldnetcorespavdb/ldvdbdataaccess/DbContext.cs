using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Data;

namespace ldvdbdal
{
    public class DbContext : IDisposable
    {

        private bool disposed = false;

        private readonly IDbConnection _connection;
        private readonly IConnectionFactory _connectionFactory;        
        private readonly LinkedList<AdoNetUnitOfWork> _uows = new LinkedList<AdoNetUnitOfWork>();

        public DbContext(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
            _connection = _connectionFactory.Create();
        }

        public IUnitOfWork CreateUnitOfWork()
        {
            var transaction = _connection.BeginTransaction();
            var uow = new AdoNetUnitOfWork(transaction, RemoveTransaction, RemoveTransaction);

            _uows.AddLast(uow);

            return uow;
        }

        public IDbCommand CreateCommand()
        {
            var cmd = _connection.CreateCommand();
            
            if (_uows.Count > 0)
                cmd.Transaction = _uows.First.Value.Transaction;

            return cmd;
        }

        private void RemoveTransaction(AdoNetUnitOfWork obj)
        {
            _uows.Remove(obj);
        }

        public void Dispose()
        {
            System.Diagnostics.Debugger.Launch();

            // Dispose of unmanaged resources.
            Dispose(true);
            // Suppress finalization.
            GC.SuppressFinalize(this);            
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                _connection.Dispose();
                // Free any other managed objects here.
                //
            }

            disposed = true;
        }
    }
}
