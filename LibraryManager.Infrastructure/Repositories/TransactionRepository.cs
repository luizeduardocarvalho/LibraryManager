namespace LibraryManager.Infrastructure.Repositories;

public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
{
    private readonly LibraryManagerDbContext context;

    public TransactionRepository(LibraryManagerDbContext context)
        : base(context)
    {
        this.context = context;
    }

    public override async Task<IEnumerable<Transaction>> GetAll()
    {
        var entity = await GetAllWithBookAndStudent().ToListAsync();
        return entity;
    }

    public async Task<IEnumerable<Transaction>> GetAllByBook(long bookId)
    {
        var getAllQuery = GetAllWithBookAndStudent();
        var entity = await getAllQuery.Where(x => x.BookId == bookId).ToListAsync();
        return entity;
    }

    public Transaction GetActiveByBook(long bookId)
    {
        return this.table.Include(x => x.Student).FirstOrDefault(x => x.Active && x.BookId == bookId);
    }

    private IIncludableQueryable<Transaction, Student> GetAllWithBookAndStudent()
    {
        var query = this.table.Include(x => x.Book).Include(x => x.Student);
        return query;
    }

    public async Task<IEnumerable<LateBookWithStudentNameDto>> GetLateBooksWithStudentName(long teacherId)
    {
        var lateBooks = await this.context.Transactions
            .Include(x => x.Book)
            .Include(x => x.Student)
            .Where(x => x.Student.TeacherId == teacherId)
            .Where(x => x.ReturnedAt == null && DateTime.Now >= x.ReturnDate)
            .Select(x =>
                new LateBookWithStudentNameDto
                {
                    BookId = x.Book.Id,
                    BookTitle = x.Book.Title,
                    StudentName = x.Student.Name
                })
            .ToListAsync();

        return lateBooks;
    }

    public async Task<IEnumerable<Transaction>> GetAllActiveTransactions()
    {
        return await this.context.Transactions.Where(x => x.Active).ToListAsync();
    }

    public async Task<IEnumerable<GetTransactionDto>> GetTransactionsWithDetailsByStudent(long studentId)
    {
        return await this.context.Transactions
            .Include(x => x.Book)
            .Include(x => x.Student)
            .Where(x => x.Student.Id == studentId)
            .Select(x =>
                new GetTransactionDto
                {
                    StudentName = x.Student.Name,
                    ReturnedAt = x.ReturnedAt,
                    BookTitle = x.Book.Title,
                    CreationDate = x.LendDate,
                    ReturnDate = x.ReturnDate,
                    TransactionId = x.Id,
                    BookId = x.Book.Id
                }).OrderByDescending(x => x.CreationDate)
            .ToListAsync();
    }

    public async Task<bool> HasBookBorrowed(long studentId)
    {
        var result = await this.context.Transactions
            .Where(x => x.StudentId == studentId && x.Active)
            .FirstOrDefaultAsync();

        return result is not null;
    }
}
