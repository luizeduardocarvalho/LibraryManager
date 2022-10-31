namespace LibraryManager.Infrastructure.Services;

public class TeacherService : ITeacherService
{
    private readonly ITeacherRepository repository;
    private readonly ILogger<TeacherService> logger;

    public TeacherService(
        ITeacherRepository repository,
        ILogger<TeacherService> logger)
    {
        this.repository = repository;
        this.logger = logger;
    }

    public async Task<IEnumerable<GetTeacherDto>> GetAll()
    {
        try
        {
            return await this.repository.GetAll();
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while getting all teachers.");
            throw;
        }
    }

    public async Task<bool> Create(CreateTeacherDto teacher)
    {
        var newTeacher = new Teacher
        {
            Name = teacher.Name,
            Email = teacher.Email,
            Password = teacher.Password,
            Role = teacher.Role
        };

        try
        {
            this.repository.Insert(newTeacher);
            var result = await this.repository.Save();

            return result;
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while creating the teacher {teacherName}.", teacher.Name);
            throw;
        }
    }

    public async Task<Teacher> GetByEmailAndPassword(string email, string password)
    {
        try
        {
            var teacher = await this.repository.GetByEmailAndPassword(email, password);

            return teacher;
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while getting the teacher {email} info.", email);
            throw;
        }
    }

    public async Task<IEnumerable<GetTeacherWithStudentsDto>> GetTeachersWithStudents()
    {
        try
        {
            return await this.repository.GetTeachersWithStudents();
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while getting the teacher list.");
            throw;
        }
    }

    public async Task Delete(long id)
    {
        try
        {
            var teacher = await this.repository.GetById(id);

            if (teacher != null)
            {
                await this.repository.Delete(teacher);
            }
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while deleting the teacher {teacherId}.", id);
            throw;
        }
    }

    public async Task<bool> UpdateTeacher(UpdateTeacherDto updateTeacher)
    {
        try
        {
            var teacher = await this.repository.GetById(updateTeacher.Id);

            if (teacher is not null)
            {
                teacher.Email = updateTeacher.Email;
                teacher.Name = updateTeacher.Name;
                teacher.Role = updateTeacher.Role;

                return await this.repository.Save();
            }

            return false;
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while updating the teacher {teacherName}.", updateTeacher.Name);
            throw;
        }
    }

    public async Task<GetTeacherDto> GetById(long id)
    {
        try
        {
            return await this.repository.GetTeacherById(id);
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while getting the teacher {teacherId}.", id);
            throw;
        }
    }
}
