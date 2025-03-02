namespace LibraryManager.Domain.Dtos;

public class CreateBookDto
{
    [Required]
    public int AuthorId { get; set; }

    public string Description { get; set; }

    public int Reference { get; set; }

    [Required]
    public string Title { get; set; }
}
