namespace LibraryManager.Domain.Dtos.Books;

public class LateBookWithStudentNameDto
{
    public long BookId { get; set; }
    public string BookTitle { get; set; }
    public string StudentName { get; set; }
}
