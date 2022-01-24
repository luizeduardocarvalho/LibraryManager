using LibraryManager.Domain.Dtos;
using LibraryManager.Domain.Dtos.Books;
using LibraryManager.Domain.Dtos.Transactions;
using LibraryManager.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManager.Domain.Abstractions.Services
{
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
    }
}
