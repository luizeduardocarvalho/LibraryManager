namespace LibraryManager.Infrastructure.Services;

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository repository;
    private readonly ILogger<AuthorService> logger;

    public AuthorService(
        IAuthorRepository repository,
        ILogger<AuthorService> logger)
    {
        this.repository = repository;
        this.logger = logger;
    }

    public async Task<IEnumerable<Author>> GetAll()
    {
        return await this.repository.GetAll();
    }

    public async Task<IEnumerable<GetAuthorDto>> GetAuthorsByName(string authorName)
    {
        return await this.repository.GetAuthorsByName(authorName);
    }

    async Task<IList<GetAuthorDto>> IAuthorService.GetSimpleAuthors()
    {
        try
        {
            return await this.repository.GetSimpleAuthors().ConfigureAwait(false);
        }
        catch(Exception e)
        {
            this.logger.LogError(e, "An error occurred while getting simple authors");
            throw;
        }
    }

    public async Task<bool> Create(CreateAuthorDto author)
    {
        var newAuthor = new Author
        {
            Name = author.Name
        };

        try
        {
            this.repository.Insert(newAuthor);
            return await this.repository.Save();
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while creating the author {authorName}", author.Name);
            throw;
        }
    }

    public async Task<GetAuthorsWithBooksDto> GetAuthorWithBooksById(long authorId)
    {
        return await this.repository.GetAuthorWithBooksById(authorId);
    }
}
