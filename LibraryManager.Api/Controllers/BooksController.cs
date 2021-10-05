namespace LibraryManager.Api.Controllers
{
    using LibraryManager.Domain.Entities;
    using LibraryManager.Infrastructure;
    using Microsoft.AspNetCore.Mvc;
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
            var book = new Book
            {
                Author = "Luiz",
                Title = "Author"
            };

            this.context.Books.Add(book);
            this.context.SaveChanges();

            return Ok(this.context.Books.First());
        }
    }
}
