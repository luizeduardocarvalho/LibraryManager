using LibraryManager.Domain.Abstractions.Services;
using LibraryManager.Domain.Entities;
using LibraryManager.Infrastructure.Repositories.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManager.Infrastructure.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository repository;

        public TransactionService(ITransactionRepository repository)
        {
            this.repository = repository;
        }

        async Task<IEnumerable<Transaction>> ITransactionService.GetAll()
        {
            var transactions = await this.repository.GetAll();
            return transactions;
        }

        async Task<IEnumerable<Transaction>> ITransactionService.GetAllByBook(long bookId)
        {
            var transactions = await this.repository.GetAllByBook(bookId);
            return transactions;
        }

        bool ITransactionService.RentBook(long bookId, long studentId)
        {
            throw new System.NotImplementedException();
        }

        bool ITransactionService.ReturnBook()
        {
            throw new System.NotImplementedException();
        }
    }
}
