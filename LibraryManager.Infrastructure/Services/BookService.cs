namespace LibraryManager.Infrastructure.Services;

public class BookService : IBookService
{
    private readonly IBookRepository bookRepository;
    private readonly ITransactionRepository transactionRepository;
    private readonly ILogger<BookService> logger;

    public BookService(
        IBookRepository bookRepository,
        ITransactionRepository transactionRepository,
        ILogger<BookService> logger)
    {
        this.bookRepository = bookRepository;
        this.transactionRepository = transactionRepository;
        this.logger = logger;
    }

    public async Task<bool> Create(CreateBookDto book)
    {
        var newBook = new Book
        {
            AuthorId = book.AuthorId,
            Title = book.Title,
            Description = book.Description,
            Reference = book.Reference
        };

        try
        {
            this.bookRepository.Insert(newBook);
            return await this.bookRepository.Save();
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while creating the book {bookTitle}.", book.Title);
            throw;
        }
    }

    public async Task<IEnumerable<Book>> GetAll()
    {
        try
        {
            var books = await this.bookRepository.GetAll();

            return books;
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while getting all books.");
            throw;
        }
    }

    public async Task<bool> LendBook(LendBookDto lendBookDto)
    {
        try
        {
            var activeTransactionsForBook = this.transactionRepository.GetActiveByBook(lendBookDto.BookId);

            var hasBookBorrowed = await this.transactionRepository.HasBookBorrowed(lendBookDto.StudentId);

            if (activeTransactionsForBook == null && !hasBookBorrowed)
            {
                var transaction = new Transaction
                {
                    BookId = lendBookDto.BookId,
                    StudentId = lendBookDto.StudentId,
                    LendDate = DateTimeOffset.Now.UtcDateTime,
                    ReturnDate = DateTimeOffset.Now.UtcDateTime.AddDays(14)
                };

                this.transactionRepository.Insert(transaction);
                return await this.bookRepository.Save();
            }
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while lending the book {bookId}.", lendBookDto.BookId);
            throw;
        }

        return false;
    }

    public async Task<GetTransactionDto> ReturnBook(long bookId)
    {
        try
        {

            var transaction = this.transactionRepository.GetActiveByBook(bookId);
            GetTransactionDto transactionDto = new() { };

            if (transaction != null)
            {
                transaction.Active = false;
                transaction.ReturnedAt = DateTimeOffset.Now.UtcDateTime;
                await this.transactionRepository.Save();

                transactionDto = new()
                {
                    ReturnedAt = transaction.ReturnedAt,
                    CreationDate = transaction.CreateDate,
                    StudentName = transaction.Student.Name,
                    TransactionId = transaction.Id,
                    ReturnDate = transaction.ReturnDate
                };
            }

            return transactionDto;

        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while returning the book {bookId}.", bookId);
            throw;
        }
    }

    public async Task<GetTransactionDto> RenewBook(long bookId)
    {
        var transaction = this.transactionRepository.GetActiveByBook(bookId);
        GetTransactionDto transactionDto = new() { };

        if (transaction != null)
        {
            try
            {
                transaction.ReturnDate = DateTimeOffset.Now.UtcDateTime.AddDays(7);
                await this.transactionRepository.Save();
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while renewing the book {bookId}.", bookId);
                throw;
            }

            transactionDto = new()
            {
                ReturnedAt = transaction.ReturnedAt,
                CreationDate = transaction.CreateDate,
                StudentName = transaction.Student.Name,
                TransactionId = transaction.Id,
                ReturnDate = transaction.ReturnDate,
                BookTitle = transaction.Book.Title
            };
        }

        return transactionDto;
    }


    public async Task<IEnumerable<GetBooksDto>> GetBooksByTitle(string title)
    {
        try
        {
            return await this.bookRepository.GetBooksByTitle(title);
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while getting the book {bookTitle}.", title);
            throw;
        }
    }

    public async Task<GetBookDto> GetBookById(long bookId)
    {
        try
        {
            return await this.bookRepository.GetBookById(bookId);
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while getting the book {bookId}.", bookId);
            throw;
        }
    }

    public async Task<bool> UpdateBook(UpdateBookDto updateBook)
    {
        try
        {
            var book = await this.bookRepository.GetById(updateBook.Id);
            if (book != null)
            {
                if (!string.IsNullOrEmpty(updateBook.Title))
                {
                    book.Title = updateBook.Title;
                }

                if (!string.IsNullOrEmpty(updateBook.Description))
                {
                    book.Description = updateBook.Description;
                }

                return await this.bookRepository.Save();
            }

            return false;
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while updating the book {bookTitle}.", updateBook.Title);
            throw;
        }
    }

    public async Task<GetBooksDto> GetBookDetailsById(long id)
    {
        try
        {
            return await this.bookRepository.GetBookDetailsById(id);
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while getting the book {bookId} details.", id);
            throw;
        }
    }
}
