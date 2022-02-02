﻿using LibraryManager.Api.Configurations;
using LibraryManager.Domain.Abstractions.Services;
using LibraryManager.Domain.Dtos;
using LibraryManager.Domain.Dtos.Books;
using LibraryManager.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace LibraryManager.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService service;
        private readonly IDistributedCache cache;
        private readonly IOptions<Settings> settings;

        public BooksController(IBookService service, IDistributedCache cache)
        {
            this.service = service;
            this.cache = cache;
            this.settings = settings;
        }

        [AllowAnonymous]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var cachedBooks = cache.GetString("Books");
            IEnumerable<Book> books;

            if (cachedBooks == null)
            {
                books = await this.service.GetAll();
                cache.SetString("Books", JsonSerializer.Serialize(books));
            }
            else
            {
                books = JsonSerializer.Deserialize<IEnumerable<Book>>(cachedBooks);
            }

            if (!books.Any())
            {
                return NotFound("No books have been found.");
            }

            return Ok(books);
        }

        [HttpPost("Lend")]
        public async Task<IActionResult> LendBook([FromBody] LendBookDto lendBookDto)
        {
            if (lendBookDto == null)
            {
                return BadRequest();
            }

            try
            {
                var result = await this.service.LendBook(lendBookDto);
                return Ok("Success");
            }
            catch
            {
                return StatusCode(500, "Something went wrong.");
            }
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

            if (result != null)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateBookDto createBookDto)
        {
            if (createBookDto == null)
            {
                return BadRequest();
            }

            try
            {
                var result = await this.service.Create(createBookDto);
                return Ok("Success");
            }
            catch
            {
                return StatusCode(500, "An error has occurred.");
            }
        }

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

            return Ok(result);
        }
    }
}
