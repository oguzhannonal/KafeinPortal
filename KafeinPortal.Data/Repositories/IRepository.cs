using KafeinPortal.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KafeinPortal.Data.Repositories
{
    public interface IRepository<T> where T : class
    {
        public T Get(Expression<Func<T, bool>> predicate);
        public IQueryable<T> GetAll();
        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate);
        public void Add(T entity);
        public void Delete(T entity);
        public void Update(T entity);


    }
}
