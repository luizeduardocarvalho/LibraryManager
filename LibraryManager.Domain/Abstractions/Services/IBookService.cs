namespace LibraryManager.Domain.Abstractions.Services;

public interface IBookService
{
    Task<IEnumerable<Book>> GetAll();
    Task<bool> Create(CreateBookDto book);
    Task<bool> LendBook(LendBookDto lendBook);
    Task<GetTransactionDto> ReturnBook(long bookId);
    Task<GetTransactionDto> RenewBook(long bookId);
    Task<IEnumerable<GetBooksDto>> GetBooksByTitle(string title);
    Task<GetBookDto> GetBookById(long bookId);
    Task<bool> UpdateBook(UpdateBookDto updateBook);
    Task<GetBooksDto> GetBookDetailsById(long id);
}
