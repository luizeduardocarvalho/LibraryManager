namespace LibraryManager.Infrastructure.Repositories.Abstractions;

public class AuthorRepository : BaseRepository<Author>, IAuthorRepository
{
    private readonly LibraryManagerDbContext context;

    public AuthorRepository(LibraryManagerDbContext context)
        : base(context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<GetAuthorDto>> GetAuthorsByName(string authorName)
    {
        return await this.context.Authors
            .Where(x => x.Name.ToLower().Contains(authorName.ToLower()))
            .Select(x => 
                new GetAuthorDto
                {
                    AuthorId = x.Id,
                    Name = x.Name
                })
            .ToListAsync();
    }

    async Task<IList<GetAuthorDto>> IAuthorRepository.GetSimpleAuthors()
    {
        return await this.context.Authors
            .Select(x => new GetAuthorDto
            {
                AuthorId = x.Id,
                Name = x.Name
            })
            .ToListAsync()
            .ConfigureAwait(false);
    }

    public async Task<GetAuthorsWithBooksDto> GetAuthorWithBooksById(long authorId)
    {
        return await this.context.Authors
            .Include(x => x.Books)
            .ThenInclude(x => x.Transactions)
            .Where(x => x.Id == authorId)
            .Select(x =>
                new GetAuthorsWithBooksDto
                {
                    AuthorId = x.Id,
                    Name = x.Name,
                    Books = x.Books.Select(b => 
                        new GetBooksDto 
                        {
                            BookId = b.Id,
                            Description = b.Description,
                            Reference = b.Reference,
                            Status = b.Transactions.OrderBy(x => x.LendDate).Last().ReturnedAt != null,
                            Title = b.Title
                        }).ToList()
                })
            .FirstOrDefaultAsync();
    }
}
