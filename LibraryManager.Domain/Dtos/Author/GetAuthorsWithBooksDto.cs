namespace LibraryManager.Domain.Dtos.Author;

public class GetAuthorsWithBooksDto
{
    public long AuthorId { get; set; }
    public string Name { get; set; }
    public IEnumerable<GetBooksDto> Books { get; set; }
}
