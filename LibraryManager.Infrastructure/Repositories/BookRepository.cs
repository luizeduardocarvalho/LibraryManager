namespace LibraryManager.Infrastructure.Repositories;

public class BookRepository : BaseRepository<Book>, IBookRepository
{
    private readonly LibraryManagerDbContext context;

    public BookRepository(LibraryManagerDbContext context)
        : base(context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<GetBooksDto>> GetBooksByTitle(string title)
    {
        return await this.context.Books
            .Include(x => x.Author)
            .Where(x => x.Title.ToLower().Contains(title.ToLower()) || x.Reference.ToString() == title)
            .Select(x =>
                new GetBooksDto
                {
                    AuthorName = x.Author.Name,
                    BookId = x.Id,
                    Description = x.Description,
                    Reference = x.Reference,
                    Title = x.Title,
                    Status = (x.Transactions.OrderBy(x => x.LendDate).Last().ReturnedAt != null || !x.Transactions.Any())
                })
            .ToListAsync();
    }

    async Task<GetBookDto> IBookRepository.GetByReference(int reference)
    {
        return await this.context.Books
            .Where(x => x.Reference == reference)
            .Select(x => new GetBookDto
            {
                BookId = x.Id,
                Description = x.Description,
                Title = x.Title,
            })
            .FirstOrDefaultAsync();
    }

    public async Task<GetBooksDto> GetBookDetailsById(long id)
    {
        return await this.context.Books
            .Where(x => x.Id == id)
            .Select(x => new GetBooksDto
            {
                AuthorName = x.Author.Name,
                AuthorId = x.Author.Id,
                BookId = x.Id,
                Description = x.Description,
                Reference = x.Reference,
                Status = (x.Transactions.OrderBy(x => x.LendDate).Last().ReturnedAt != null || !x.Transactions.Any()),
                Title = x.Title,
            })
            .FirstOrDefaultAsync();
    }

    public async Task<GetBookDto> GetBookById(long bookId)
    {
        return await this.context.Books
            .Include(x => x.Transactions)
            .ThenInclude(x => x.Student)
            .Where(x => x.Id == bookId)
            .Select(x =>
                new GetBookDto
                {
                    BookId = x.Id,
                    Description = x.Description,
                    Title = x.Title,
                    Transactions = x.Transactions.Select(x =>
                        new GetTransactionDto
                        {
                            StudentName = x.Student.Name,
                            CreationDate = x.LendDate,
                            ReturnedAt = x.ReturnedAt,
                            ReturnDate = x.ReturnDate,
                            TransactionId = x.Id
                        }).OrderByDescending(x => x.TransactionId).ToList()
                }).FirstOrDefaultAsync();
    }
}
