using LibraryManager.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManager.Domain.Abstractions.Services
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAll();
    }
}
