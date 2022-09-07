namespace LibraryManager.Infrastructure.Repositories;

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
            .Where(x => x.Students.Any(s => s.Transactions.Any(t => t.Active)))
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
                            })
                })
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
