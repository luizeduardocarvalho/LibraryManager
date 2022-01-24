using LibraryManager.Domain.Dtos.Author;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManager.Domain.Abstractions.Services
{
    public interface IAuthorService
    {
        Task<IEnumerable<GetAuthorDto>> GetAuthorsByName(string authorName);
        Task<bool> Create(CreateAuthorDto author);
    }
}
