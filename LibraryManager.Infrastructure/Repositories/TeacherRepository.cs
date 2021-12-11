using LibraryManager.Domain.Entities;
using LibraryManager.Infrastructure.Repositories.Abstractions;

namespace LibraryManager.Infrastructure.Repositories
{
    public class TeacherRepository : BaseRepository<Teacher>, ITeacherRepository
    {
        public TeacherRepository(LibraryManagerDbContext context)
            : base(context)
        {
        }
    }
}
