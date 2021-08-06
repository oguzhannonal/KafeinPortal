using KafeinPortal.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeinPortal.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly EfContext _efContext;



        public UnitOfWork(EfContext efContext)
        {
            _efContext = efContext;

        }
        public IRepository<T> GetRepository<T>() where T : class, new()
        {
            return new Repository<T>(_efContext);
        }
        public int SaveChanges()
        {
            var transaction = _efContext.Database.BeginTransaction();
            try
            {
                 

                var affectedRow = _efContext.SaveChanges();
                transaction.Commit();
                return affectedRow;
            }
            catch (Exception ex)
            {

                transaction.Rollback();
                return -1;
            }
        }
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _efContext.Dispose();
            }
        }
    }
}
