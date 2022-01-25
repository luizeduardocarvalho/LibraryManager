using LibraryManager.Domain.Dtos.Author;
using LibraryManager.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManager.Domain.Abstractions.Services
{
    public interface IAuthorService
    {
        Task<IEnumerable<GetAuthorDto>> GetAuthorsByName(string authorName);
        Task<bool> Create(CreateAuthorDto author);
        Task<GetAuthorsWithBooksDto> GetAuthorWithBooksById(long authorId);
        Task<IEnumerable<Author>> GetAll();
    }
}
