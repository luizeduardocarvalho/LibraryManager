using LibraryManager.Domain.Dtos.Author;
using LibraryManager.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManager.Infrastructure.Repositories.Abstractions
{
    public interface IAuthorRepository : IBaseRepository<Author>
    {
        Task<IEnumerable<GetAuthorDto>> GetAuthorsByName(string authorName);
    }
}
