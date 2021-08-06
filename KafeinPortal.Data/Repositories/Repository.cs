using KafeinPortal.Data.Context;
using KafeinPortal.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KafeinPortal.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        private readonly EfContext _context;
        private readonly DbSet<T> _dbSet;
        public Repository(EfContext context)
        {
            _dbSet = context.Set<T>();
            _context = context;
        }
        public T Get(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).AsNoTracking().FirstOrDefault();
        }
        public IQueryable<T> GetAll()
        {
            return _dbSet.AsNoTracking();
        }
        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).AsNoTracking();
        }
        public void Add(T entity)
        {
            
            _dbSet.Add(entity);

        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
        }
    }
}
