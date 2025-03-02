namespace LibraryManager.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TransactionsController : ControllerBase
{
    private readonly ITransactionService service;

    public TransactionsController(ITransactionService service)
    {
        this.service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GeAll()
    {
        var transactions = await this.service.GetAll();

        if (!transactions.Any())
        {
            return NotFound("No transactions have been found.");
        }

        return Ok(transactions);
    }

    [HttpGet("GetAllActive")]
    public async Task<IActionResult> GetAllActiveTransactions()
    {
        var transactions = await this.service.GetAllActiveTransactions();

        return Ok(transactions);
    }

    [HttpGet("GetAllByBook")]
    public async Task<IActionResult> GetAllByBook([FromQuery] long bookId)
    {
        var transactions = await this.service.GetAllByBook(bookId);

        if (!transactions.Any())
        {
            return NotFound("Not transaction found.");
        }

        return Ok(transactions);
    }

    [HttpGet("GetLateBooks")]
    public async Task<IActionResult> GetLateBooksWithStudentName([FromQuery] long teacherId)
    {
        var lateBooks = await this.service.GetLateBooksWithStudentName(teacherId);

        return Ok(lateBooks);
    }

    [HttpGet("GetTransactionsWithDetailsByStudent")]
    public async Task<IActionResult> GetTransactionsWithDetailsByStudent([FromQuery] long studentId)
    {
        if (studentId == 0)
        {
            return BadRequest();
        }

        var transactions = await this.service.GetTransactionsWithDetailsByStudent(studentId);

        return Ok(transactions);
    }

    [HttpGet("GetMostLentBooks")]
    public async Task<IActionResult> GetMostLentBooks()
    {
        var result = await this.service.GetMostLentBooks();

        return Ok(result);
    }

    [HttpGet("GetLeastLentBooks")]
    public async Task<IActionResult> GetLeastLentBooks()
    {
        var result = await this.service.GetMostLentBooks();

        return Ok(result);
    }
}
