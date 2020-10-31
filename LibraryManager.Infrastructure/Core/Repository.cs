using LibraryManager.Domain.Entities.Core;
using LibraryManager.Domain.Repositories.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace LibraryManager.Infrastructure.Core
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly DbContext dbContext;
        private readonly DbSet<TEntity> dbSet;

        public Repository(LibraryManagerContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = dbContext.Set<TEntity>();
        }

        public bool Insert(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            dbSet.Add(entity);
            Commit();

            return true;
        }

        public IQueryable<TEntity> Query()
        {
            return dbSet.AsNoTracking();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && dbContext != null)
            {
                dbContext.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Commit()
        {
            dbContext.SaveChanges();
        }
    }
}
