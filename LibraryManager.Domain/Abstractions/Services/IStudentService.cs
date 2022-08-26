namespace LibraryManager.Domain.Abstractions.Services;

public interface IStudentService
{
    Task<IEnumerable<Student>> GetAll();
    Task<bool> Create(CreateStudentDto student);
    Task<IEnumerable<StudentsWithBooksDto>> GetStudentsWithBooksByTeacher(long teacherId);
    Task<IEnumerable<GetStudentsDto>> GetStudentsByName(string name);
    Task<GetStudentWithTransactionsDto> GetStudentWithTransactionsById(long studentId);
    Task<bool> UpdateStudentTeacher(UpdateStudentTeacherDto updateStudentDto);
}
