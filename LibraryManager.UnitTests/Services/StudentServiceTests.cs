using LibraryManager.Domain.Dtos.Students;
using LibraryManager.Infrastructure.Repositories.Abstractions;
using LibraryManager.Infrastructure.Services;
using Microsoft.Extensions.Logging;

namespace LibraryManager.UnitTests.Services;

[TestClass]
public sealed class StudentServiceTests
{
    private IStudentRepository studentRepository;
    private ITeacherRepository teacherRepository;
    private TestableLogger<StudentService> logger;
    private StudentService studentService;

    [TestInitialize]
    public void Initialize()
    {
        studentRepository = Substitute.For<IStudentRepository>();
        teacherRepository = Substitute.For<ITeacherRepository>();
        logger = Substitute.For<TestableLogger<StudentService>>();

        studentService = new StudentService(
            studentRepository,
            teacherRepository,
            logger);
    }

    [TestMethod]
    public async Task GetAllShouldReturnStudents()
    {
        // Arrange
        var expectedStudents = new List<Student>()
        {
            new()
            {
                Id = 1,
                Name = "Luiz"
            }
        };

        this.studentRepository.GetAll().Returns(expectedStudents);

        // Act
        var actualResult = await this.studentService.GetAll();

        // Arrange
        actualResult.Should().BeEquivalentTo(expectedStudents);
    }

    [TestMethod]
    public async Task GetAllWithErrorShouldThrowException()
    {
        // Arrange
        var exception = new Exception();
        this.studentRepository.GetAll().ThrowsAsync(exception);

        // Act
        Func<Task> act = () => this.studentService.GetAll();

        // Arrange
        await act.Should().ThrowAsync<Exception>();
        logger.Received(1).Log(LogLevel.Error, exception, "An error occurred while getting all students.");
    }

    [TestMethod]
    public async Task GetStudentsWithBooksByTeacherShouldReturnStudents()
    {
        // Arrange
        var teacherId = 1;
        var expectedStudents = new List<StudentsWithBooksDto>
        {
            new()
            {
                StudentId = 1,
                NumberOfActiveBooks = 1,
                Name = "Student Name"
            }
        };

        studentRepository.GetStudentsWithBooksByTeacher(teacherId).Returns(expectedStudents);

        // Act
        var actualResult = await this.studentService.GetStudentsWithBooksByTeacher(teacherId);

        // Assert
        actualResult.Should().BeEquivalentTo(expectedStudents);
    }

    [TestMethod]
    public async Task GetStudentsWithBooksByTeacherWithErrorShouldThrowException()
    {
        // Arrange
        var teacherId = 1;
        var exception = new Exception();
        this.studentRepository.GetStudentsWithBooksByTeacher(teacherId).ThrowsAsync(exception);

        // Act
        Func<Task> act = () => this.studentService.GetStudentsWithBooksByTeacher(teacherId);

        // Arrange
        await act.Should().ThrowAsync<Exception>();
        logger
            .Received(1)
            .Log(
                LogLevel.Error,
                exception,
                "An error occurred while getting the student list for the teacher {teacherId}.",
                teacherId);
    }
}
