using LibraryManager.Infrastructure.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManager.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly LibraryManagerDbContext context;
        public DbSet<T> table = null;

        public BaseRepository(LibraryManagerDbContext context)
        {
            this.context = context;
            table = this.context.Set<T>();
        }

        void IBaseRepository<T>.Delete(object id)
        {
            throw new NotImplementedException();
        }

        void Dispose()
        {
            throw new NotImplementedException();
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            var entity = await this.table.ToListAsync();
            return entity;
        }

        T IBaseRepository<T>.GetById(object id)
        {
            throw new NotImplementedException();
        }

        void IBaseRepository<T>.Insert(T obj)
        {
            throw new NotImplementedException();
        }

        void IBaseRepository<T>.Save()
        {
            throw new NotImplementedException();
        }

        void IBaseRepository<T>.Update(T obj)
        {
            throw new NotImplementedException();
        }
    }
}
