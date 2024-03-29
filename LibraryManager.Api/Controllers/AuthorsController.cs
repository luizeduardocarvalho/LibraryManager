﻿namespace LibraryManager.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthorsController : ControllerBase
{
    private readonly IAuthorService service;

    public AuthorsController(IAuthorService service)
    {
        this.service = service;
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var authors = await this.service.GetAll();

        return Ok(authors);
    }

    [HttpGet("GetSimpleAuthors")]
    public async Task<IActionResult> GetSimpleAuthors()
    {
        var authors = await this.service.GetSimpleAuthors();

        return Ok(authors);
    }

    [HttpGet("GetAuthorsByName")]
    public async Task<IActionResult> GetAuthorsByName([FromQuery] string authorName)
    {
        if (authorName == null)
        {
            return BadRequest();
        }

        var authors = await this.service.GetAuthorsByName(authorName);

        return Ok(authors);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create([FromBody] CreateAuthorDto author)
    {
        var result = await this.service.Create(author);

        if (result)
        {
            return Ok();
        }

        return StatusCode(500, "Unexpected error");
    }

    [HttpGet("GetAuthorWithBooksById")]
    public async Task<IActionResult> GetAuthorsWithBookById([FromQuery] long authorId)
    {
        if (authorId == 0)
        {
            return BadRequest();
        }

        var author = await this.service.GetAuthorWithBooksById(authorId);

        return Ok(author);
    }

    [HttpPut]
    public async Task<IActionResult> Edit(EditAtuhorDto editAuthorDto)
    {
        await this.service.Edit(editAuthorDto);

        return Ok();
    }

    [HttpGet("GetAuthorById")]
    public async Task<IActionResult> GetAuthorById(long id)
    {
        var author = await this.service.GetAuthorById(id);

        return Ok(author);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] long id)
    {
        await this.service.Delete(id);

        return Ok();
    }
}
