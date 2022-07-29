using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Autogermana.Test.Data.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task DeleteByIdAsync(object id);

        void Delete(TEntity entity);

        Task<IEnumerable<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null
        );

        Task<TEntity> GetByIdAsync(object id);

        Task<TEntity> GetFirstOrDefaultAsync(
            Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = null
        );

        void Insert(TEntity entity);
    }
}
