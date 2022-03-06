using LibraryManager.Domain.Dtos.Teacher;
using LibraryManager.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManager.Infrastructure.Repositories.Abstractions
{
    public interface ITeacherRepository : IBaseRepository<Teacher>
    {
        Task<IEnumerable<GetTeacherDto>> GetAll();
        Task<Teacher> GetByEmailAndPassword(string email, string password);
        Task<IEnumerable<GetTeacherWithStudentsDto>> GetTeachersWithStudents();
        Task<Teacher> GetEntityById(long id);
        Task<bool> Delete(Teacher teacher);
        Task<int> GetLastReference();
        Task<Teacher> GetByReference(int reference);
        Task<GetTeacherDto> GetById(long id);
    }
}
