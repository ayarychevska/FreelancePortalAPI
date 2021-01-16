using Core.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Core.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _context;
        protected readonly DbSet<TEntity> _entities;

        public Repository(DbContext context)
        {
            _context = context;
            _entities = context.Set<TEntity>();
        }

        public virtual TEntity Add(TEntity entity)
        {
            var result = _entities.Add(entity);
            _context.SaveChanges();
            return result.Entity;
        }

        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            _entities.AddRange(entities);
            _context.SaveChanges();
        }


        public virtual TEntity Update(TEntity entity)
        {
            var result = _entities.Update(entity);
            _context.SaveChanges();
            return result.Entity;
        }

        public virtual void UpdateRange(IEnumerable<TEntity> entities)
        {
            _entities.UpdateRange(entities);
        }



        public virtual void Remove(TEntity entity)
        {
            _entities.Remove(entity);
            _context.SaveChanges();
        }

        public virtual void RemoveRange(IEnumerable<TEntity> entities)
        {
            _entities.RemoveRange(entities);
            _context.SaveChanges();
        }


        public virtual int Count()
        {
            return _entities.Count();
        }


        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.Where(predicate).AsEnumerable();
        }

        public virtual IQueryable<TEntity> FindQuery(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.Where(predicate);
        }

        public virtual TEntity GetSingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities
                .SingleOrDefault(predicate);
        }

        public virtual TEntity GetSingleOrDefaultInclude(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = _entities
                .Where(predicate);

            foreach (var include in includes)
            {
                query.Include(include);
            }

            return query.SingleOrDefault();
        }

        public virtual TEntity Get(int id)
        {
            return _entities.Find(id);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return _entities.ToList();
        }
    }
}
