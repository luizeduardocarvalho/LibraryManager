namespace LibraryManager.Domain.Abstractions.Services;

public interface ITransactionService
{
    Task<IEnumerable<Transaction>> GetAll();
    Task<IEnumerable<Transaction>> GetAllByBook(long bookId);
    Task<IEnumerable<LateBookWithStudentNameDto>> GetLateBooksWithStudentName(long teacherId);
    Task<IEnumerable<Transaction>> GetAllActiveTransactions();
    Task<IEnumerable<GetTransactionDto>> GetTransactionsWithDetailsByStudent(long studentId);
}
