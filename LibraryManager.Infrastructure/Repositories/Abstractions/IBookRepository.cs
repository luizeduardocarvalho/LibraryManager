﻿namespace LibraryManager.Infrastructure.Repositories.Abstractions;

public interface IBookRepository : IBaseRepository<Book>
{
    Task<IEnumerable<GetBooksDto>> GetBooksByTitle(string title);
    Task<GetBookDto> GetBookById(long bookId);
    Task<GetBooksDto> GetBookDetailsById(long id);
    Task<GetBookDto> GetByReference(int reference);
}
