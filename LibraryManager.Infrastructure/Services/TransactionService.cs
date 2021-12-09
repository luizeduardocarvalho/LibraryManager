using LibraryManager.Domain.Abstractions.Services;
using LibraryManager.Domain.Entities;
using LibraryManager.Infrastructure.Repositories.Abstractions;
using System.Collections.Generic;

namespace LibraryManager.Infrastructure.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository repository;

        public TransactionService(ITransactionRepository repository)
        {
            this.repository = repository;
        }

        IEnumerable<Transaction> ITransactionService.GetAll()
        {
            var transactions = this.repository.GetAll();
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
