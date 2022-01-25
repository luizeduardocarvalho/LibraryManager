using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManager.Infrastructure.Repositories.Abstractions
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        void Insert(T obj);
        void Update(T obj);
        void Delete(object id);
        Task<bool> Save();
    }
}
