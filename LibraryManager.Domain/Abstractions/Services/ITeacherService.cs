namespace LibraryManager.Domain.Abstractions.Services;

public interface ITeacherService
{
    Task<IEnumerable<GetTeacherDto>> GetAll();
    Task<bool> Create(CreateTeacherDto teacher);
    Task<User> GetByEmailAndPassword(string email, string password);
    Task<IEnumerable<GetTeacherWithStudentsDto>> GetTeachersWithStudents();
    Task Delete(long id);
    Task<bool> UpdateTeacher(UpdateTeacherDto updateTeacher);
    Task<GetTeacherDto> GetById(long id);
}
