namespace LibraryManager.Api.Controllers
{
    using LibraryManager.Domain.Abstractions.Services;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using System.Threading.Tasks;

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

            if(!transactions.Any())
            {
                return NotFound("No transactions have been found.");
            }

            return Ok(transactions);
        }

        [HttpGet("GetAllByBook")]
        public async Task<IActionResult> GetAllByBook([FromQuery] long bookId)
        {
            var transactions = await this.service.GetAllByBook(bookId);

            if(!transactions.Any())
            {
                return NotFound("Not transaction found.");
            }

            return Ok(transactions);
        }
    }
}
