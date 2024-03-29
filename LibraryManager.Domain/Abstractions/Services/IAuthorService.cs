﻿namespace LibraryManager.Domain.Abstractions.Services;

public interface IAuthorService
{
    Task<IEnumerable<GetAuthorDto>> GetAuthorsByName(string authorName);
    Task<bool> Create(CreateAuthorDto author);
    Task<GetAuthorsWithBooksDto> GetAuthorWithBooksById(long authorId);
    Task<IEnumerable<Author>> GetAll();
    Task<IList<GetAuthorDto>> GetSimpleAuthors();
    Task Edit(EditAtuhorDto editAuthorDto);
    Task<GetAuthorDto> GetAuthorById(long id);
    Task Delete(long id);
}
