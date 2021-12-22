using LibraryManager.Domain.Abstractions.Services;
using LibraryManager.Domain.Dtos;
using LibraryManager.Domain.Entities;
using LibraryManager.Infrastructure.Repositories.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManager.Infrastructure.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository repository;

        public BookService(IBookRepository repository)
        {
            this.repository = repository;
        }

        public async Task<bool> Create(CreateBookDto book)
        {
            var newBook = new Book
            {
                AuthorId = book.AuthorId,
                Title = book.Title,
                Description = book.Description
            };

            this.repository.Insert(newBook);
            var result = await this.repository.Save();

            return result;
        }

        public async Task<IEnumerable<Book>> GetAll()
        {
            var books = await this.repository.GetAll();
            return books;
        }
    }
}
