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

        public StudentService(IStudentRepository repository)
        {
            this.repository = repository;
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
                Name = student.Name,
                TeacherId = student.TeacherId
            };

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

            if(student is not null)
            {
                student.TeacherId = updateStudentDto.TeacherId;
                return await this.repository.Save();
            }

            return false;
        }
    }
}
