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

            this.repository.Insert(newStudent);
            var result = await this.repository.Save();

            return result;
        }
    }
}
