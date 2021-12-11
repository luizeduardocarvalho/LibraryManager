using LibraryManager.Domain.Entities;
using LibraryManager.Infrastructure.Repositories.Abstractions;

namespace LibraryManager.Infrastructure.Repositories
{
    public class StudentRepository : BaseRepository<Student>, IStudentRepository
    {
        public StudentRepository(LibraryManagerDbContext context)
            : base(context)
        {
        }
    }
}
