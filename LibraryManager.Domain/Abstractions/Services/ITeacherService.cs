using LibraryManager.Domain.Dtos;
using LibraryManager.Domain.Dtos.Teacher;
using LibraryManager.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManager.Domain.Abstractions.Services
{
    public interface ITeacherService
    {
        Task<IEnumerable<Teacher>> GetAll();
        Task<bool> Create(CreateTeacherDto teacher);
        Task<Teacher> GetByEmailAndPassword(string email, string password);
        Task<IEnumerable<GetTeacherWithStudentsDto>> GetTeachersWithStudents();
        Task<bool> Delete(long id);
    }
}
