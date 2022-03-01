using LibraryManager.Domain.Abstractions.Services;
using LibraryManager.Domain.Dtos.Students;
using LibraryManager.Domain.Entities;
using LibraryManager.Infrastructure.Repositories.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManager.Infrastructure.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository repository;
        private readonly ITeacherRepository teacherRepository;

        public StudentService(IStudentRepository repository, ITeacherRepository teacherRepository)
        {
            this.repository = repository;
            this.teacherRepository = teacherRepository;
        }

        public async Task<IEnumerable<Student>> GetAll()
        {
            var students = await this.repository.GetAll();
            return students;
        }

        public async Task<bool> Create(CreateStudentDto student)
        {
            var newStudent = new Student
            {
                Name = student.Name
            };

            if(student.TeacherId != 0)
            {
                newStudent.TeacherId = student.TeacherId;
            }

            try
            {
                this.repository.Insert(newStudent);
                return await this.repository.Save();
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<StudentsWithBooksDto>> GetStudentsWithBooksByTeacher(long teacherId)
        {
            return await this.repository.GetStudentsWithBooksByTeacher(teacherId);
        }
        
        public async Task<IEnumerable<GetStudentsDto>> GetStudentsByName(string name)
        {
            return await this.repository.GetStudentsByName(name);
        }

        public async Task<GetStudentWithTransactionsDto> GetStudentWithTransactionsById(long studentId)
        {
            return await this.repository.GetStudentWithTransactionsById(studentId);
        }

        public async Task<bool> UpdateStudentTeacher(UpdateStudentTeacherDto updateStudentDto)
        {
            var student = await this.repository.GetById(updateStudentDto.StudentId);

            var teacher = await this.teacherRepository.GetByReference(updateStudentDto.TeacherId);

            if(student is not null && teacher is not null)
            {
                student.TeacherId = teacher.Id;
                return await this.repository.Save();
            }

            return false;
        }
    }
}
