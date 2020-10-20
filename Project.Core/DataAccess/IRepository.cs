using Microsoft.EntityFrameworkCore.Query;
using Project.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Project.Core.DataAccess
{
    public interface IRepository<TEntity> where TEntity : class, IEntity, new()
    {
        TEntity Get(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);
        List<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int page = 1,
            int pageSize = 0,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null
            //params Expression<Func<TEntity, object>>[] includedProperties
            );
        int GetListCount(Expression<Func<TEntity, bool>> filter = null);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
