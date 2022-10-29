namespace LibraryManager.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class BooksController : ControllerBase
{
    private readonly IBookService service;

    public BooksController(
        IBookService service)
    {
        this.service = service ?? throw new ArgumentNullException(nameof(service));
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var books = await this.service.GetAll();

        if (!books.Any())
        {
            return NotFound("No books have been found.");
        }

        return Ok(books);
    }

    [HttpPost("Lend")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status424FailedDependency)]
    public async Task<IActionResult> LendBook([FromBody] LendBookDto lendBookDto)
    {
        if (lendBookDto == null)
        {
            return BadRequest();
        }

        var result = await this.service.LendBook(lendBookDto);

        if (result)
        {
            return Ok();
        }

        return StatusCode(400, "Could not lend book.");

    }

    [HttpPost("Return")]
    public async Task<IActionResult> ReturnBook([FromBody] ReturnBookDto returnBookDto)
    {
        if (returnBookDto == null)
        {
            return BadRequest();
        }

        var result = await this.service.ReturnBook(returnBookDto.BookId);

        if (result != null)
        {
            return Ok(result);
        }

        return BadRequest();
    }

    [HttpPatch("Renew")]
    public async Task<IActionResult> RenewBook([FromBody] ReturnBookDto returnBookDto)
    {
        if (returnBookDto == null)
        {
            return BadRequest();
        }

        var result = await this.service.RenewBook(returnBookDto.BookId);

        if (!string.IsNullOrEmpty(result.BookTitle))
        {
            return Ok(result);
        }

        return StatusCode(500, "An error has occurred.");
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create([FromBody] CreateBookDto createBookDto)
    {
        if (createBookDto == null)
        {
            return BadRequest();
        }

        await this.service.Create(createBookDto);

        return StatusCode(201, "Book created");
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetBooksByTitle([FromQuery] string title)
    {
        if (title == null)
        {
            return BadRequest();
        }

        var books = await this.service.GetBooksByTitle(title);

        return Ok(books);
    }

    [HttpGet("GetBookById")]
    public async Task<IActionResult> GetBookById([FromQuery] long bookId)
    {
        if (bookId == 0)
        {
            return BadRequest();
        }

        var book = await this.service.GetBookById(bookId);

        return Ok(book);
    }

    [HttpPatch("UpdateBook")]
    public async Task<IActionResult> UpdateBook([FromBody] UpdateBookDto updateBook)
    {
        if (updateBook is null)
        {
            return BadRequest();
        }

        var result = await this.service.UpdateBook(updateBook);

        if (result)
        {
            return Ok("Success!");
        }

        return StatusCode(500, "An error has occurred.");
    }

    [HttpGet("GetBookDetails")]
    public async Task<IActionResult> GetBookDetails([FromQuery] long id)
    {
        var result = await this.service.GetBookDetailsById(id);

        return Ok(result);
    }
}
