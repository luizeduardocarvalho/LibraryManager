namespace LibraryManager.Domain.Dtos.Students;

public class UpdateStudentTeacherDto
{
    public string StudentName { get; set; }
    public long StudentId { get; set; }
    public int TeacherId { get; set; }
}
