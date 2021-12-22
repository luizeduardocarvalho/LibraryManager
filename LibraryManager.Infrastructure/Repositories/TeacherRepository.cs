using LibraryManager.Domain.Entities;
using LibraryManager.Infrastructure.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
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
    }
}
