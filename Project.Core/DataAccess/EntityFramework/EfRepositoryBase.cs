using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Project.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Project.Core.DataAccess.EntityFramework
{
    public class EfRepositoryBase<TEntity, TContext> : IRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext
    {
        private TContext _dbContext;

        public EfRepositoryBase(TContext dbContext)
        {
            _dbContext = dbContext;
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();

            if (include != null)
            {
                query = include(query);
            }

            return query.SingleOrDefault(filter);
        }

        public List<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int page = 1,
            int pageSize = 0,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        //params Expression<Func<TEntity, object>>[] includedProperties)
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();

            if (filter != null)
            { query = query.Where(filter); }

            if (include != null)
            {
                query = include(query);
            }
            //foreach (var includeProperty in includedProperties)
            //{ query = query.Include(includeProperty); }

            if (orderBy != null)
            { query = orderBy(query); }

            if (pageSize > 0)
            { query = query.Skip((page - 1) * pageSize).Take(pageSize); }

            return query.ToList();
        }

        public int GetListCount(Expression<Func<TEntity, bool>> filter = null
            //,
            //Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            //int page = 1,
            //int pageSize = 0,
            //Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
            //params Expression<Func<TEntity, object>>[] includedProperties
            )
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();

            if (filter != null)
            { query = query.Where(filter); }

            //if (include != null)
            //{
            //    query = include(query);
            //}
            //foreach (var includeProperty in includedProperties)
            //{ query = query.Include(includeProperty); }

            //if (pageSize > 0)
            //{ query = query.Take(pageSize).Skip((page - 1) * pageSize); }

            //if (orderBy != null)
            //{ return orderBy(query).ToList().Count(); }

            //else
            //{ 
            return query.ToList().Count();
            //}
        }

        public void Add(TEntity entity)
        {
            var addedEntity = _dbContext.Entry(entity);
            addedEntity.State = EntityState.Added;
            //_dbContext.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            var updatedEntity = _dbContext.Entry(entity);
            updatedEntity.State = EntityState.Modified;
            //updatedEntity.Property("Inserted").IsModified = false;
            //_dbContext.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            var deleteEntity = _dbContext.Entry(entity);
            deleteEntity.State = EntityState.Deleted;
            //deleteEntity.Property("Deleted").IsModified = true;
            //deleteEntity.Property("TitleName").IsModified = false;
            //_dbContext.SaveChanges();
        }

    }
}
