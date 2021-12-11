using LibraryManager.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManager.Domain.Abstractions.Services
{
    public interface ITransactionService
    {
        bool RentBook(long bookId, long studentId);
        bool ReturnBook();
        Task<IEnumerable<Transaction>> GetAll();
        Task<IEnumerable<Transaction>> GetAllByBook(long bookId);
    }
}
