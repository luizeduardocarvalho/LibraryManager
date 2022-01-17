using LibraryManager.Domain.Dtos.Books;
using LibraryManager.Domain.Dtos.Transactions;
using LibraryManager.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManager.Infrastructure.Repositories.Abstractions
{
    public interface ITransactionRepository : IBaseRepository<Transaction>
    {
        Task<IEnumerable<Transaction>> GetAllByBook(long bookId);
        Transaction GetActiveByBook(long bookId);
        Task<IEnumerable<LateBookWithStudentNameDto>> GetLateBooksWithStudentName();
        Task<IEnumerable<Transaction>> GetAllActiveTransactions();
        Task<IEnumerable<GetTransactionDto>> GetTransactionsWithDetailsByStudent(long studentId);
    }
}
