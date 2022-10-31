namespace LibraryManager.Infrastructure.Repositories.Abstractions;

public interface IBaseRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAll();
    void Insert(T obj);
    void Update(T obj);
    Task<bool> Save();
    Task Delete(T obj);
    Task<T> GetById(long id);
}
