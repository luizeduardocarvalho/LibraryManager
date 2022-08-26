namespace LibraryManager.UnitTests.Controllers;

[TestClass]
public class BooksControllerTests
{
    private IBookService bookService;
    private BooksController controller;
    private Author author;

    [TestInitialize]
    public void Initialize()
    {
        bookService = Substitute.For<IBookService>();
        controller = new BooksController(bookService);
        author = new Author
        {
            Id = 1,
            Name = "Author"
        };
    }

    [TestMethod]
    public async Task GetAllShouldReturnOk()
    {
        // Arrange
        var books = new List<Book>
        {
            new Book
            {
                Id = 1,
                Title = "Book Title",
                Author = this.author,
                Description = "Book Description",
                Reference = 1,
                CheckoutPeriod = 7
            }
        };

        bookService.GetAll().Returns(books);

        // Act
        var expectedBooks = await controller.GetAll() as OkObjectResult;

        // Assert
        expectedBooks.StatusCode.Should().Be(200);
        expectedBooks.Value.Should().Be(books);
    }
}
