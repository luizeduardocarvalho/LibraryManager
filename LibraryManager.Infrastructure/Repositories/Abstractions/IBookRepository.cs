using LibraryManager.Domain.Dtos.Books;
using LibraryManager.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManager.Infrastructure.Repositories.Abstractions
{
    public interface IBookRepository : IBaseRepository<Book>
    {
        Task<IEnumerable<GetBooksDto>> GetBooksByTitle(string title);
    }
}
