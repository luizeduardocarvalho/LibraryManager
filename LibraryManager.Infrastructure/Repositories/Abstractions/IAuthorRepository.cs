namespace LibraryManager.Infrastructure.Repositories.Abstractions;

public interface IAuthorRepository : IBaseRepository<Author>
{
    Task<GetAuthorDto> GetAuthorById(long id);
    Task<IEnumerable<GetAuthorDto>> GetAuthorsByName(string authorName);
    Task<GetAuthorsWithBooksDto> GetAuthorWithBooksById(long authorId);
    Task<IList<GetAuthorDto>> GetSimpleAuthors();
}
