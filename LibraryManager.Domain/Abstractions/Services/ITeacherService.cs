namespace LibraryManager.Domain.Abstractions.Services;

public interface ITeacherService
{
    Task<IEnumerable<GetTeacherDto>> GetAll();
    Task<bool> Create(CreateTeacherDto teacher);
    Task<Teacher> GetByEmailAndPassword(string email, string password);
    Task<IEnumerable<GetTeacherWithStudentsDto>> GetTeachersWithStudents();
    Task<bool> Delete(long id);
    Task<bool> UpdateTeacher(UpdateTeacherDto updateTeacher);
    Task<GetTeacherDto> GetById(long id);
}
