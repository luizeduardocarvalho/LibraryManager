using LibraryManager.Domain.Dtos.Students;
using LibraryManager.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManager.Domain.Abstractions.Services
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetAll();
        Task<bool> Create(CreateStudentDto student);
        Task<IEnumerable<StudentsWithBooksDto>> GetStudentsWithBooksByTeacher(long teacherId);
        Task<IEnumerable<GetStudentsDto>> GetStudentsByName(string name);
    }
}
