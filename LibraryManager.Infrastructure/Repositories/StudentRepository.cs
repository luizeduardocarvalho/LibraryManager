﻿using LibraryManager.Domain.Dtos.Students;
using LibraryManager.Domain.Dtos.Transactions;
using LibraryManager.Domain.Entities;
using LibraryManager.Infrastructure.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManager.Infrastructure.Repositories
{
    public class StudentRepository : BaseRepository<Student>, IStudentRepository
    {
        private readonly LibraryManagerDbContext context;

        public StudentRepository(LibraryManagerDbContext context)
            : base(context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<StudentsWithBooksDto>> GetStudentsWithBooksByTeacher(long teacherId)
        {
            var students = await this.context.Students
                                                .Where(x => x.TeacherId == teacherId)
                                                .Select(x =>
                                                    new StudentsWithBooksDto
                                                    {
                                                        StudentId = x.Id,
                                                        Name = x.Name,
                                                        NumberOfActiveBooks = x.Transactions.Where(x => x.Active).Count()
                                                    })
                                                .ToListAsync();

            return students;
        }

        public async Task<IEnumerable<GetStudentsDto>> GetStudentsByName(string name)
        {
            var students = await this.context.Students
                                                .Where(x => x.Name.ToLower().Contains(name.ToLower()))
                                                .Select(x =>
                                                    new GetStudentsDto
                                                    {
                                                        StudentId = x.Id,
                                                        Name = x.Name
                                                    })
                                                .ToListAsync();

            return students;
        }

        public async Task<GetStudentWithTransactionsDto> GetStudentWithTransactionsById(long studentId)
        {
            return await this.context.Students
                                            .Where(x => x.Id == studentId)
                                            .Select(x =>
                                                new GetStudentWithTransactionsDto
                                                {
                                                    StudentId = x.Id,
                                                    StudentName = x.Name,
                                                    Transactions = x.Transactions.Select(x =>
                                                        new GetTransactionDto {
                                                            ReturnedAt = x.ReturnedAt,
                                                            BookId = x.BookId,
                                                            BookTitle = x.Book.Title,
                                                            CreationDate = x.LendDate,
                                                            ReturnDate = x.ReturnDate,
                                                            TransactionId = x.Id,
                                                            IsLate = (DateTimeOffset.Now >= x.ReturnDate && x.ReturnedAt == null)
                                                        }).OrderByDescending(x => x.CreationDate).ToList()
                                                })
                                            .FirstOrDefaultAsync();
        }

        public async Task<Student> GetById(long id)
        {
            return await this.context.Students.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
