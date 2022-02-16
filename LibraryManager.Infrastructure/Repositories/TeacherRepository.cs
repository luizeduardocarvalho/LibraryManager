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
                                                                        BookId = t.BookId,
                                                                        BookTitle = t.Book.Title,
                                                                        ReturnedAt = t.ReturnedAt,
                                                                        TransactionId = t.Id,
                                                                        CreationDate = t.LendDate,
                                                                        ReturnDate = t.ReturnDate
                                                                    }).ToList()
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
    }
}
