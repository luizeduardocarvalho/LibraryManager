namespace LibraryManager.Api.Controllers
{
    using LibraryManager.Domain.Entities;
    using LibraryManager.Infrastructure;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly LibraryManagerDbContext context;

        public BooksController(LibraryManagerDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var transaction = new Transaction
            {
                BookId = 1,
                StudentId = 1
            };

            this.context.Transactions.Add(transaction);
            this.context.SaveChanges();

            return Ok(this.context.Transactions.Include(x => x.Student).Include(x => x.Book).First());
        }
    }
}
