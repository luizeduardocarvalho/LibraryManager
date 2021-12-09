namespace LibraryManager.Api.Controllers
{
    using LibraryManager.Domain.Abstractions.Services;
    using Microsoft.AspNetCore.Mvc;
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
            var transactions = service.GetAll();

            return Ok(transactions);
        }
    }
}
