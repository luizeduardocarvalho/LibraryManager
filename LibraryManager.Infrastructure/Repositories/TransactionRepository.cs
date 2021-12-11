using LibraryManager.Domain.Entities;
using LibraryManager.Infrastructure.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManager.Infrastructure.Repositories
{
    public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
    {
        private readonly LibraryManagerDbContext context;

        public TransactionRepository(LibraryManagerDbContext context)
            : base(context)
        {
            this.context = context;
        }

        public override async Task<IEnumerable<Transaction>> GetAll()
        {
            var entity = await GetAllWithBookAndStudent().ToListAsync();
            return entity;
        }

        public async Task<IEnumerable<Transaction>> GetAllByBook(long bookId)
        {
            var getAllQuery = GetAllWithBookAndStudent();
            var entity = await getAllQuery.Where(x => x.BookId == bookId).ToListAsync();
            return entity;
        }

        private IIncludableQueryable<Transaction, Student> GetAllWithBookAndStudent()
        {
            var query = this.table.Include(x => x.Book).Include(x => x.Student);
            return query;
        }
    }
}
