using LibraryManager.Domain.Entities;
using LibraryManager.Infrastructure.Repositories.Abstractions;

namespace LibraryManager.Infrastructure.Repositories
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        public BookRepository(LibraryManagerDbContext context) 
            : base(context)
        {
        }
    }
}
