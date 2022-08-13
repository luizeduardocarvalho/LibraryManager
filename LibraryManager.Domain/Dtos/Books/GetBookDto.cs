namespace LibraryManager.Domain.Dtos.Books;

public class GetBookDto
{
    public long BookId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public IEnumerable<GetTransactionDto> Transactions { get; set; }
}
