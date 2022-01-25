using LibraryManager.Domain.Abstractions.Services;
using LibraryManager.Domain.Dtos.Author;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LibraryManager.Api.Controllers
{
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

        [HttpGet("GetAuthorsByName")]
        public async Task<IActionResult> GetAuthorsByName([FromQuery] string authorName)
        {
            if(authorName == null)
            {
                return BadRequest();
            }

            var authors = await this.service.GetAuthorsByName(authorName);

            return Ok(authors);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateAuthorDto author)
        {
            if(author is null)
            {
                return BadRequest();
            }

            var result = await this.service.Create(author);

            if(result)
            {
                return Ok("Author was created");
            }

            return StatusCode(500, "Unexpected error");
        }

        [HttpGet("GetAuthorWithBooksById")]
        public async Task<IActionResult> GetAuthorsWithBookById([FromQuery] long authorId)
        {
            if(authorId == 0)
            {
                return BadRequest();
            }

            var author = await this.service.GetAuthorWithBooksById(authorId);

            return Ok(author);
        }
    }
}
