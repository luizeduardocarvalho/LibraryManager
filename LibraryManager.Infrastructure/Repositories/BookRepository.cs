using LibraryManager.Domain.Dtos.Books;
using LibraryManager.Domain.Dtos.Transactions;
using LibraryManager.Domain.Entities;
using LibraryManager.Infrastructure.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManager.Infrastructure.Repositories
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        private readonly LibraryManagerDbContext context;

        public BookRepository(LibraryManagerDbContext context) 
            : base(context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<GetBooksDto>> GetBooksByTitle(string title)
        {
            return await this.context.Books
                                        .Include(x => x.Author)
                                        .Where(x => x.Title.ToLower().Contains(title.ToLower()))
                                        .Select(x => 
                                            new GetBooksDto
                                            {
                                                AuthorName = x.Author.Name,
                                                BookId = x.Id,
                                                Description = x.Description,
                                                Title = x.Title,
                                                Status = x.Transactions.OrderBy(x => x.LendDate).Last().ReturnedAt != null
                                            })
                                        .ToListAsync();
        }

        public async Task<GetBookDto> GetBookById(long bookId)
        {
            return await this.context.Books
                                        .Include(x => x.Transactions)
                                        .ThenInclude(x => x.Student)
                                        .Where(x => x.Id == bookId)
                                        .Select(x =>
                                            new GetBookDto
                                            {
                                                BookId = x.Id,
                                                Description = x.Description,
                                                Title = x.Title,
                                                Transactions = x.Transactions.Select(x =>
                                                    new GetTransactionDto
                                                    {
                                                        StudentName = x.Student.Name,
                                                        CreationDate = x.LendDate,
                                                        ReturnedAt = x.ReturnedAt,
                                                        ReturnDate = x.ReturnDate,
                                                        TransactionId = x.Id
                                                    }).OrderByDescending(x => x.TransactionId).ToList()
                                            }).FirstOrDefaultAsync();
        }

        public async Task<Book> GetById(long bookId)
        {
            return await this.context.Books.FirstOrDefaultAsync(x => x.Id == bookId);
        }
    }
}
