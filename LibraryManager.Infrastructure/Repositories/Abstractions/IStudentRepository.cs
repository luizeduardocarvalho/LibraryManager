namespace LibraryManager.Infrastructure.Repositories.Abstractions;

public interface IStudentRepository : IBaseRepository<Student>
{
    Task<IEnumerable<StudentsWithBooksDto>> GetStudentsWithBooksByTeacher(long teacherId);
    Task<IEnumerable<GetStudentsDto>> GetStudentsByName(string name);
    Task<GetStudentWithTransactionsDto> GetStudentWithTransactionsById(long studentId);
}
