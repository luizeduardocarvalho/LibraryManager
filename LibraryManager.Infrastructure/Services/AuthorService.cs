using LibraryManager.Domain.Abstractions.Services;
using LibraryManager.Domain.Dtos.Author;
using LibraryManager.Domain.Entities;
using LibraryManager.Infrastructure.Repositories.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManager.Infrastructure.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository repository;

        public AuthorService(IAuthorRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<Author>> GetAll()
        {
            return await this.repository.GetAll();
        }

        public async Task<IEnumerable<GetAuthorDto>> GetAuthorsByName(string authorName)
        {
            return await this.repository.GetAuthorsByName(authorName);
        }

        public async Task<bool> Create(CreateAuthorDto author)
        {
            var newAuthor = new Author
            {
                Name = author.Name
            };

            this.repository.Insert(newAuthor);
            var result = await this.repository.Save();

            return result;
        }

        public async Task<GetAuthorsWithBooksDto> GetAuthorWithBooksById(long authorId)
        {
            return await this.repository.GetAuthorWithBooksById(authorId);
        }
    }
}
