using LibraryManager.Domain.Entities;
using Microsoft.Extensions.Logging;
using System.Xml.Linq;

namespace LibraryManager.Infrastructure.Services;

public class StudentService : IStudentService
{
    private readonly IStudentRepository repository;
    private readonly ITeacherRepository teacherRepository;
    private readonly ILogger<StudentService> logger;

    public StudentService(
        IStudentRepository repository,
        ITeacherRepository teacherRepository,
        ILogger<StudentService> logger)
    {
        this.repository = repository;
        this.teacherRepository = teacherRepository;
        this.logger = logger;
    }

    public async Task<IEnumerable<Student>> GetAll()
    {
        try
        {
            var students = await this.repository.GetAll();
            return students;
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while getting all students. {message}", e.Message);
            throw;
        }
    }

    public async Task<bool> Create(CreateStudentDto student)
    {
        var newStudent = new Student
        {
            Name = student.Name
        };

        if (student.TeacherId != 0)
        {
            newStudent.TeacherId = student.TeacherId;
        }

        try
        {
            this.repository.Insert(newStudent);
            return await this.repository.Save();
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while creating the student {studentName}.", student.Name);
            throw;
        }
    }

    public async Task<IEnumerable<StudentsWithBooksDto>> GetStudentsWithBooksByTeacher(long teacherId)
    {
        try
        {
            return await this.repository.GetStudentsWithBooksByTeacher(teacherId);
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while gettings the student list for the teacher {teacherId}.", teacherId);
            throw;
        }
    }

    public async Task<IEnumerable<GetStudentsDto>> GetStudentsByName(string name)
    {
        try
        {
            return await this.repository.GetStudentsByName(name);
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while getting the students by the name {studentName}.", name);
            throw;
        }
    }

    public async Task<GetStudentWithTransactionsDto> GetStudentWithTransactionsById(long studentId)
    {
        try
        {
            return await this.repository.GetStudentWithTransactionsById(studentId);
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while getting the student list for the student {studentName}.", studentId);
            throw;
        }
    }

    public async Task<bool> UpdateStudentTeacher(UpdateStudentTeacherDto updateStudentDto)
    {
        try
        {
            var student = await this.repository.GetById(updateStudentDto.StudentId);

            var teacher = await this.teacherRepository.GetByReference(updateStudentDto.TeacherId);

            if (student is not null && teacher is not null)
            {
                if (!string.IsNullOrEmpty(updateStudentDto.StudentName))
                    student.Name = updateStudentDto.StudentName;
                student.TeacherId = teacher.Id;
                return await this.repository.Save();
            }

            return false;
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while updating the student {studentName}'s teacher.", updateStudentDto.StudentName);
            throw;
        }
    }
}
