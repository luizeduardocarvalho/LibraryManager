namespace LibraryManager.Domain.Dtos.Students;

public class StudentsWithBooksDto
{
    public long StudentId { get; set; }
    public string Name { get; set; }
    public int NumberOfActiveBooks { get; set; }
}
