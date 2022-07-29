using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Autogermana.Test.Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;
        internal DbSet<TEntity> dbSet;

        public Repository(DbContext context)
        {
            Context = context;
            this.dbSet = context.Set<TEntity>();
        }


        public async Task DeleteByIdAsync(object id)
        {
            TEntity entityToDelete = await dbSet.FindAsync(id);
            Delete(entityToDelete);
        }


        public void Delete(TEntity entityToDelete)
        {
            if (Context.Entry(entityToDelete).State == EntityState.Detached)
                dbSet.Attach(entityToDelete);

            dbSet.Remove(entityToDelete);
        }


        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = null)
        {
            IQueryable<TEntity> query = dbSet;
            if (filter != null)
                query = query.Where(filter);

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    query = query.Include(includeProperty);
            }

            if (orderBy != null)
                return await orderBy(query).ToListAsync();

            return await query.ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(object id)
        {
            return await dbSet.FindAsync(id);
        }


        public async Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<TEntity> query = dbSet;
            if (filter != null)
                query = query.Where(filter);

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    query = query.Include(includeProperty);
            }

            return await query.FirstOrDefaultAsync();
        }


        public void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }
    }
}
