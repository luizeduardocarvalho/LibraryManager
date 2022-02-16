using LibraryManager.Infrastructure.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        void Dispose()
        {
            throw new NotImplementedException();
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            var entity = await this.table.ToListAsync();
            return entity;
        }

        public async void Insert(T obj)
        {
            await this.table.AddAsync(obj);
        }

        public async Task<bool> Save()
        {
            var result = await this.context.SaveChangesAsync();

            if (result > 0)
            {
                return true;
            }

            return false;
        }

        void IBaseRepository<T>.Update(T obj)
        {
            throw new NotImplementedException();
        }
    }
}
