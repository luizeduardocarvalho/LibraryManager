using LibraryManager.Domain.Abstractions.Services;
using LibraryManager.Domain.Dtos;
using LibraryManager.Domain.Dtos.Books;
using LibraryManager.Domain.Dtos.Transactions;
using LibraryManager.Domain.Entities;
using LibraryManager.Infrastructure.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManager.Infrastructure.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository bookRepository;
        private readonly ITransactionRepository transactionRepository;

        public BookService(
            IBookRepository bookRepository,
            ITransactionRepository transactionRepository)
        {
            this.bookRepository = bookRepository;
            this.transactionRepository = transactionRepository;
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
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<Book>> GetAll()
        {
            var books = await this.bookRepository.GetAll();
            return books;
        }

        public async Task<bool> LendBook(LendBookDto lendBookDto)
        {
            var activeTransactionsForBook = this.transactionRepository.GetActiveByBook(lendBookDto.BookId);

            if (activeTransactionsForBook == null)
            {
                var transaction = new Transaction
                {
                    BookId = lendBookDto.BookId,
                    StudentId = lendBookDto.StudentId,
                    LendDate = DateTimeOffset.Now,
                    ReturnDate = DateTimeOffset.Now.AddDays(14)
                };

                try
                {
                    this.transactionRepository.Insert(transaction);
                    return await this.bookRepository.Save();
                }
                catch
                {
                    throw;
                }
            }

            return false;
        }

        public async Task<GetTransactionDto> ReturnBook(long bookId)
        {
            var transaction = this.transactionRepository.GetActiveByBook(bookId);
            GetTransactionDto transactionDto = new() { };

            if (transaction != null)
            {
                transaction.Active = false;
                transaction.ReturnedAt = DateTimeOffset.Now;
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

        public async Task<GetTransactionDto> RenewBook(long bookId)
        {
            var transaction = this.transactionRepository.GetActiveByBook(bookId);
            GetTransactionDto transactionDto = new() { };

            if (transaction != null)
            {
                transaction.ReturnDate = DateTimeOffset.Now.AddDays(7);
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


        public async Task<IEnumerable<GetBooksDto>> GetBooksByTitle(string title)
        {
            return await this.bookRepository.GetBooksByTitle(title);
        }

        public async Task<GetBookDto> GetBookById(long bookId)
        {
            return await this.bookRepository.GetBookById(bookId);
        }

        public async Task<bool> UpdateBook(UpdateBookDto updateBook)
        {
            var book = await this.bookRepository.GetById(updateBook.Id);
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
    }
}
