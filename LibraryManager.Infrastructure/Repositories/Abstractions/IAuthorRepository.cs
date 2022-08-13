namespace LibraryManager.Infrastructure.Repositories.Abstractions;

public interface IAuthorRepository : IBaseRepository<Author>
{
    Task<IEnumerable<GetAuthorDto>> GetAuthorsByName(string authorName);
    Task<GetAuthorsWithBooksDto> GetAuthorWithBooksById(long authorId);
}
