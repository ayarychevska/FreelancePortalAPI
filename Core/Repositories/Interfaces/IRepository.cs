using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Core.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        TEntity Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);

        int Count();

        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> FindQuery(Expression<Func<TEntity, bool>> predicate);
        TEntity GetSingleOrDefault(Expression<Func<TEntity, bool>> predicate);
        TEntity GetSingleOrDefaultInclude(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();
    }
}
