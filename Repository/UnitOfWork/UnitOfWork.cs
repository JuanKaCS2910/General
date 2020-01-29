using System;
using System.Data;

namespace Repository.UnitOfWork
{
    public partial class UnitOfWork : IDisposable
    {
        private DBRestauranteEntities context = new DBRestauranteEntities("DBRestauranteEntities");

        public void Save()
        {
            context.SaveChanges();
        }

        public void Save(Action action)
        {
            using (var transaction = context.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    action();
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw e;
                }
            }
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
