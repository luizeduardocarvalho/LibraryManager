using LibraryManager.Domain.Dtos.Students;
using LibraryManager.Domain.Entities;
using LibraryManager.Infrastructure.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
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
                                                .Where(x => x.Name.Contains(name))
                                                .Select(x =>
                                                    new GetStudentsDto
                                                    {
                                                        StudentId = x.Id,
                                                        Name = x.Name
                                                    })
                                                .ToListAsync();

            return students;
        }
    }
}
