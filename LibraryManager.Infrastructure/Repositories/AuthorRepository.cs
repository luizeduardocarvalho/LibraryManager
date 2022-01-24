using LibraryManager.Domain.Dtos.Author;
using LibraryManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManager.Infrastructure.Repositories.Abstractions
{
    public class AuthorRepository : BaseRepository<Author>, IAuthorRepository
    {
        private readonly LibraryManagerDbContext context;

        public AuthorRepository(LibraryManagerDbContext context)
            : base(context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<GetAuthorDto>> GetAuthorsByName(string authorName)
        {
            return await this.context.Authors
                                        .Where(x => x.Name.ToLower().Contains(authorName.ToLower()))
                                        .Select(x => 
                                            new GetAuthorDto
                                            {
                                                AuthorId = x.Id,
                                                Name = x.Name
                                            })
                                        .ToListAsync();
        }
    }
}
