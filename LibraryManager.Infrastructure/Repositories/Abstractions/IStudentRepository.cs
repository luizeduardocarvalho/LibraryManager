using LibraryManager.Domain.Dtos.Students;
using LibraryManager.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManager.Infrastructure.Repositories.Abstractions
{
    public interface IStudentRepository : IBaseRepository<Student>
    {
        Task<IEnumerable<StudentsWithBooksDto>> GetStudentsWithBooksByTeacher(long teacherId);
        Task<IEnumerable<GetStudentsDto>> GetStudentsByName(string name);
    }
}
