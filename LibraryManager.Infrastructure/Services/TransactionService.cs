using LibraryManager.Domain.Entities;

namespace LibraryManager.Infrastructure.Services;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository repository;
    private readonly ILogger<TransactionService> logger;

    public TransactionService(
        ITransactionRepository repository,
        ILogger<TransactionService> logger)
    {
        this.repository = repository;
        this.logger = logger;
    }

    public async Task<IEnumerable<Transaction>> GetAll()
    {
        try
        {
            return await this.repository.GetAll();
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while all transactions.");
            throw;
        }
    }

    public async Task<IEnumerable<Transaction>> GetAllActiveTransactions()
    {
        try
        {
            return await this.repository.GetAllActiveTransactions();
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while getting all active transactions.");
            throw;
        }
    }


    public async Task<IEnumerable<Transaction>> GetAllByBook(long bookId)
    {
        try
        {
            var transactions = await this.repository.GetAllByBook(bookId);
            return transactions;
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while getting all transactions for the book {0}.", bookId);
            throw;
        }
    }

    public async Task<IEnumerable<LateBookWithStudentNameDto>> GetLateBooksWithStudentName(long teacherId)
    {
        try
        {
            return await this.repository.GetLateBooksWithStudentName(teacherId);
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while getting the late book list for the teacher {0}.", teacherId);
            throw;
        }
    }

    public async Task<IEnumerable<GetTransactionDto>> GetTransactionsWithDetailsByStudent(long studentId)
    {
        try
        {
            return await this.repository.GetTransactionsWithDetailsByStudent(studentId);
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while getting the transaction list for the student {0}.", studentId);
            throw;
        }
    }
}
