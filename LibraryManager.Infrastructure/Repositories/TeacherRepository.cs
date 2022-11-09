namespace LibraryManager.Infrastructure.Repositories;

public class TeacherRepository : BaseRepository<Teacher>, ITeacherRepository
{
    private readonly LibraryManagerDbContext context;

    public TeacherRepository(LibraryManagerDbContext context)
        : base(context)
    {
        this.context = context;
    }

    public async new Task<IEnumerable<GetTeacherDto>> GetAll()
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

    public async Task<User> GetByEmailAndPassword(string email, string password)
    {
        User teacher = await this.context.Teachers.FirstOrDefaultAsync(
            x => string.Equals(x.Email, email)
            && string.Equals(x.Password, password));

        if(teacher is null)
        {
            var user = await this.context.Students.FirstOrDefaultAsync(
                x => string.Equals(x.Email, email)
                && string.Equals(x.Password, password));

            return user;
        }

        return teacher;
    }

    // TODO: Refactor
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
                                        })
                                    .Where(x => x.IsActive)
                            })
                        .Where(x => x.Transactions.Any(t => t.IsActive))
                })
            .Where(x => x.Students.Any(s => s.Transactions.Any(t => t.IsActive)))
            .ToListAsync();
    }

    public async Task<int> GetLastReference()
    {
        return await this.context.Teachers.OrderByDescending(x => x.Reference).Select(x => x.Reference).FirstOrDefaultAsync();
    }

    public async Task<Teacher> GetByReference(int reference)
    {
        return await this.context.Teachers.Where(x => x.Reference == reference).FirstOrDefaultAsync();
    }

    public async Task<GetTeacherDto> GetTeacherById(long id)
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
