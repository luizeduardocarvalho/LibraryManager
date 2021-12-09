using LibraryManager.Domain.Entities;
using LibraryManager.Infrastructure.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

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

        public override IEnumerable<Transaction> GetAll()
        {
            var entity = this.table.Include(x => x.Book).Include(x => x.Student).ToList();
            return entity;
        }
    }
}
