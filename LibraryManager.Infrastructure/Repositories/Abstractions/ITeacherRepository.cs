using LibraryManager.Domain.Dtos.Teacher;
using LibraryManager.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManager.Infrastructure.Repositories.Abstractions
{
    public interface ITeacherRepository : IBaseRepository<Teacher>
    {
        Task<Teacher> GetByEmailAndPassword(string email, string password);
        Task<IEnumerable<GetTeacherWithStudentsDto>> GetTeachersWithStudents();
        Task<Teacher> GetEntityById(long id);
        Task<bool> Delete(Teacher teacher);
    }
}
