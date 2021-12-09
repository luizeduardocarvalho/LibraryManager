using LibraryManager.Domain.Entities;
using System.Collections.Generic;

namespace LibraryManager.Domain.Abstractions.Services
{
    public interface ITransactionService
    {
        bool RentBook(long bookId, long studentId);
        bool ReturnBook();
        IEnumerable<Transaction> GetAll();
    }
}
