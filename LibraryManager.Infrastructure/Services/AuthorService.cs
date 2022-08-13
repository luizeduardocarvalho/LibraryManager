﻿namespace LibraryManager.Infrastructure.Services;

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository repository;

    public AuthorService(IAuthorRepository repository)
    {
        this.repository = repository;
    }

    public async Task<IEnumerable<Author>> GetAll()
    {
        return await this.repository.GetAll();
    }

    public async Task<IEnumerable<GetAuthorDto>> GetAuthorsByName(string authorName)
    {
        return await this.repository.GetAuthorsByName(authorName);
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
        catch
        {
            throw new Exception("An error occurred while creating an author.");
        }
    }

    public async Task<GetAuthorsWithBooksDto> GetAuthorWithBooksById(long authorId)
    {
        return await this.repository.GetAuthorWithBooksById(authorId);
    }
}
