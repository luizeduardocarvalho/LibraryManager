using LibraryManager.Domain.Dtos;
using LibraryManager.Domain.Dtos.Books;
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
        Task<bool> ReturnBook(ReturnBookDto returnBookDto);
    }
}
