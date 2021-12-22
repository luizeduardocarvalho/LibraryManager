using LibraryManager.Domain.Entities;
using System.Threading.Tasks;

namespace LibraryManager.Infrastructure.Repositories.Abstractions
{
    public interface ITeacherRepository : IBaseRepository<Teacher>
    {
        Task<Teacher> GetByEmailAndPassword(string email, string password);
    }
}
