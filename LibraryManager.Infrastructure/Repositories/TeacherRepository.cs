using LibraryManager.Domain.Dtos.Students;
using LibraryManager.Domain.Dtos.Teacher;
using LibraryManager.Domain.Dtos.Transactions;
using LibraryManager.Domain.Entities;
using LibraryManager.Infrastructure.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManager.Infrastructure.Repositories
{
    public class TeacherRepository : BaseRepository<Teacher>, ITeacherRepository
    {
        private readonly LibraryManagerDbContext context;

        public TeacherRepository(LibraryManagerDbContext context)
            : base(context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<GetTeacherDto>> GetAll()
        {
            return await this.context.Teachers
                                        .OrderBy(x => x.Name)
                                        .Select(x =>
                                            new GetTeacherDto
                                            {
                                                Email = x.Email,
                                                Id = x.Id,
                                                Name = x.Name,
                                                Reference = x.Reference,
                                                Role = x.Role
                                            })
                                        .ToListAsync();
        }

        public async Task<Teacher> GetByEmailAndPassword(string email, string password)
        {
            var teacher = await this.context.Teachers.FirstOrDefaultAsync(
                x => string.Equals(x.Email, email) 
                && string.Equals(x.Password, password));

            return teacher;
        }

        public async Task<IEnumerable<GetTeacherWithStudentsDto>> GetTeachersWithStudents()
        {
            return await this.context.Teachers
                                        .Include(x => x.Students)
                                        .ThenInclude(x => x.Transactions)
                                        .ThenInclude(x => x.Book)
                                        .Select(x =>
                                            new GetTeacherWithStudentsDto
                                            {
                                                TeacherId = x.Id,
                                                TeacherName = x.Name,
                                                Students = x.Students
                                                    .Where(s => s.Transactions.Count > 0)
                                                    .Select(s =>
                                                        new GetStudentWithTransactionsDto
                                                        {
                                                            StudentId = s.Id,
                                                            StudentName = s.Name,
                                                            Transactions = s.Transactions
                                                                .Select(t =>
                                                                    new GetTransactionDto
                                                                    {
                                                                        IsActive = t.Active,
                                                                        BookId = t.BookId,
                                                                        BookTitle = t.Book.Title,
                                                                        ReturnedAt = t.ReturnedAt,
                                                                        TransactionId = t.Id,
                                                                        CreationDate = t.LendDate,
                                                                        ReturnDate = t.ReturnDate
                                                                    }).Where(x => x.IsActive).ToList()
                                                        }).ToList()
                                            }).ToListAsync();
        }

        public async Task<Teacher> GetEntityById(long id)
        {
            return await this.context.Teachers.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> Delete(Teacher teacher)
        {
            this.context.Remove(teacher);

            var result = await this.context.SaveChangesAsync();

            return result > 0 ? true : false;
        }

        public async Task<int> GetLastReference()
        {
            return await this.context.Teachers.OrderByDescending(x => x.Reference).Select(x => x.Reference).FirstOrDefaultAsync();
        }

        public async Task<Teacher> GetByReference(int reference)
        {
            return await this.context.Teachers.Where(x => x.Reference == reference).FirstOrDefaultAsync();
        }

        public async Task<GetTeacherDto> GetById(long id)
        {
            return await this.context.Teachers
                                        .Where(x => x.Id == id)
                                        .Select(x =>
                                            new GetTeacherDto
                                            {
                                                Email = x.Email,
                                                Id = x.Id,
                                                Name = x.Name,
                                                Reference = x.Reference,
                                                Role = x.Role
                                            })
                                        .FirstOrDefaultAsync();
        }
    }
}
