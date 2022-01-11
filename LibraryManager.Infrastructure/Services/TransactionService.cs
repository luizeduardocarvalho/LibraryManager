using LibraryManager.Domain.Abstractions.Services;
using LibraryManager.Domain.Dtos.Books;
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

        public async Task<IEnumerable<Transaction>> GetAll()
        {
            return await this.repository.GetAll();
        }

        public async Task<IEnumerable<Transaction>> GetAllActiveTransactions()
        {
            return await this.repository.GetAllActiveTransactions();
        }


        public async Task<IEnumerable<Transaction>> GetAllByBook(long bookId)
        {
            var transactions = await this.repository.GetAllByBook(bookId);
            return transactions;
        }

        public async Task<IEnumerable<LateBookWithStudentNameDto>> GetLateBooksWithStudentName()
        {
            return await this.repository.GetLateBooksWithStudentName();
        }
    }
}
