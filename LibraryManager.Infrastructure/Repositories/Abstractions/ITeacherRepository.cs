namespace LibraryManager.Infrastructure.Repositories.Abstractions;

public interface ITeacherRepository : IBaseRepository<Teacher>
{
    Task<IEnumerable<GetTeacherDto>> GetAll();
    Task<Teacher> GetByEmailAndPassword(string email, string password);
    Task<IEnumerable<GetTeacherWithStudentsDto>> GetTeachersWithStudents();
    Task<int> GetLastReference();
    Task<Teacher> GetByReference(int reference);
    Task<GetTeacherDto> GetTeacherById(long id);
    Task<Teacher> GetById(long id);
    Task<bool> Delete(Teacher teacher);
}
