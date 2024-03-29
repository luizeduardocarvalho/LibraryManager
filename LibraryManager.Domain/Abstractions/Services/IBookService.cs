﻿namespace LibraryManager.Domain.Abstractions.Services;

public interface IBookService
{
    Task<IEnumerable<Book>> GetAll();
    Task<Book> Create(CreateBookDto book);
    Task<bool> LendBook(LendBookDto lendBookDto);
    Task<GetTransactionDto> ReturnBook(long bookId);
    Task<GetTransactionDto> RenewBook(long bookId);
    Task<IEnumerable<GetBooksDto>> GetBooksByTitle(string title);
    Task<GetBookDto> GetBookById(long bookId);
    Task<bool> UpdateBook(UpdateBookDto updateBook);
    Task<GetBooksDto> GetBookDetailsById(long id);
}
