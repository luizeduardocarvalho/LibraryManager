using FluentAssertions;
using LibraryManager.Api.Controllers;
using LibraryManager.Domain.Abstractions.Services;
using LibraryManager.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using LibraryManager.Domain.Dtos.Author;
using Newtonsoft.Json;
using NSubstitute.ExceptionExtensions;

namespace LibraryManager.UnitTests.Controllers
{
    [TestClass]
    public class AuthorsControllerTests
    {
        private IAuthorService authorService;
        private AuthorsController controller;

        [TestInitialize]
        public void Initialize()
        {
            authorService = Substitute.For<IAuthorService>();
            controller = new AuthorsController(authorService);
        }

        [TestMethod]
        public async Task GetAllShouldReturnOk()
        {
            // Arrange
            var authors = new List<Author>
            {
                new Author
                {
                    Id = 1,
                    Name = "AuthorName"
                }
            };

            authorService.GetAll().Returns(authors);

            // Act
            var expectedAuthors = await controller.GetAll() as OkObjectResult;

            // Assert
            expectedAuthors.StatusCode.Should().Be(200);
            expectedAuthors.Value.Should().Be(authors);
        }

        [TestMethod]
        public async Task GetAuthorsByNameWithNullArgumentsShouldReturnBadRequest()
        {
            // Act
            var result = await controller.GetAuthorsByName(default);

            // Assert
            result.Should().BeOfType(typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task GetAuthorsByNameWithAuthorNameShouldReturnOk()
        {
            // Assert
            var authors = new List<GetAuthorDto>
            {
                new GetAuthorDto()
                {
                    AuthorId = 1,
                }
            };

            var name = "AuthorName";

            authorService.GetAuthorsByName(name).Returns(authors);

            // Act
            var result = await controller.GetAuthorsByName(name) as OkObjectResult;

            // Assert
            result.StatusCode.Should().Be(200);
            result.Value.Should().Be(authors);
        }

        [TestMethod]
        public async Task CreateWithNullParameterShouldReturnInternalServerError()
        {
            // Act
            var result = await controller.Create(default) as ObjectResult;

            // Assert
            result.StatusCode.Should().Be(500);
            result.Value.Should().Be("Unexpected error");
        }

        [TestMethod]
        public async Task CreateWithNullNameShouldReturnBadRequest()
        {
            // Arrange
            var author = new CreateAuthorDto();

            controller.ModelState.AddModelError("Name", "The Name field is required.");

            // Act
            var result = await controller.Create(author) as BadRequestObjectResult;

            // Assert
            result.StatusCode.Should().Be(400);
        }

        [TestMethod]
        public async Task CreateWithNameShouldReturnOk()
        {
            // Arrange
            var author = new CreateAuthorDto()
            {
                Name = "AuthorName"
            };

            authorService.Create(author).Returns(true);

            // Act
            var result = await controller.Create(author) as OkObjectResult;

            // Assert
            result.StatusCode.Should().Be(200);
            result.Value.Should().Be("Author was created");
        }

        [TestMethod]
        public async Task CreateWithSaveErrorShouldReturnInternalServerError()
        {
            // Arrange
            var author = new CreateAuthorDto()
            {
                Name = "AuthorName"
            };

            authorService.Create(author).Throws(x => { throw new Exception(); });

            // Act
            var result = await controller.Create(author) as ObjectResult;

            // Arrange
            result.StatusCode.Should().Be(500);
            result.Value.Should().Be("Unexpected error");
        }

        [TestMethod]
        public async Task GetAuthorWithBooksByIdWithAuthorIdZeroShouldReturnBadRequest()
        {
            // Act
            var result = await controller.GetAuthorsWithBookById(default) as BadRequestResult;

            // Assert
            result.StatusCode.Should().Be(400);
        }

        [TestMethod]
        public async Task GetAuthorWithBooksByIdShouldReturnOK()
        {
            // Arrange
            var author = new GetAuthorsWithBooksDto()
            {
                AuthorId = 1
            };

            authorService.GetAuthorWithBooksById(1).Returns(author);

            // Act
            var result = await controller.GetAuthorsWithBookById(1) as OkObjectResult;

            // Assert
            result.StatusCode.Should().Be(200);
            result.Value.Should().Be(author);
        }
    }
}
