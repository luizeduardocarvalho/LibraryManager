﻿using LibraryManager.Domain.Abstractions.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManager.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService service;

        public BooksController(IBookService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var books = await this.service.GetAll();

            if (!books.Any())
            {
                return NotFound("No books have been found.");
            }

            return Ok(books);
        }
    }
}
