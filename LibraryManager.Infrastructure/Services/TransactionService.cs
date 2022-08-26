namespace LibraryManager.Infrastructure.Services;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository repository;

    public TransactionService(ITransactionRepository repository)
    {
        this.repository = repository;
    }

    public async Task<IEnumerable<Transaction>> GetAll()
    {
        try
        {
            return await this.repository.GetAll();
        }
        catch
        {
            throw new Exception("An error occurred while all transactions.");
        }
    }

    public async Task<IEnumerable<Transaction>> GetAllActiveTransactions()
    {
        try
        {
            return await this.repository.GetAllActiveTransactions();
        }
        catch
        {
            throw new Exception("An error occurred while getting all active transactions.");
        }
    }


    public async Task<IEnumerable<Transaction>> GetAllByBook(long bookId)
    {
        try
        {
            var transactions = await this.repository.GetAllByBook(bookId);
            return transactions;
        }
        catch
        {
            throw new Exception("An error occurred while getting all transactions by book.");
        }
    }

    public async Task<IEnumerable<LateBookWithStudentNameDto>> GetLateBooksWithStudentName(long teacherId)
    {
        try
        {
            return await this.repository.GetLateBooksWithStudentName(teacherId);
        }
        catch
        {
            throw new Exception("An error occurred while getting the late book list.");
        }
    }

    public async Task<IEnumerable<GetTransactionDto>> GetTransactionsWithDetailsByStudent(long studentId)
    {
        try
        {
            return await this.repository.GetTransactionsWithDetailsByStudent(studentId);
        }
        catch
        {
            throw new Exception("An error occurred while getting the transaction list.");
        }
    }
}
