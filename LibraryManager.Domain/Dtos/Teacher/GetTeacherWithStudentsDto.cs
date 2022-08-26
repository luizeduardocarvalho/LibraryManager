namespace LibraryManager.Domain.Dtos.Teacher;

public class GetTeacherWithStudentsDto
{
    public long TeacherId { get; set; }
    public string TeacherName { get; set; }
    public IEnumerable<GetStudentWithTransactionsDto> Students { get; set; }
}
