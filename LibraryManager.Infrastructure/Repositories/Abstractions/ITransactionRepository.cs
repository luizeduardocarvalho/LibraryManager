namespace LibraryManager.Infrastructure.Repositories.Abstractions
{
    public interface ITransactionRepository : IBaseRepository<Transaction>
    {
        Task<IEnumerable<Transaction>> GetAllByBook(long bookId);
        Transaction GetActiveByBook(long bookId);
        Task<IEnumerable<LateBookWithStudentNameDto>> GetLateBooksWithStudentName(long teacherId);
        Task<IEnumerable<Transaction>> GetAllActiveTransactions();
        Task<IEnumerable<GetTransactionDto>> GetTransactionsWithDetailsByStudent(long studentId);
        Task<bool> HasBookBorrowed(long studentId);
        Task<IList<GetBookDto>> GetMostLentBooks();
        Task<IList<GetBookDto>> GetLeastLentBooks();
    }
}
