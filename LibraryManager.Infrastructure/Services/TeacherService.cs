using LibraryManager.Domain.Abstractions.Services;
using LibraryManager.Domain.Dtos;
using LibraryManager.Domain.Entities;
using LibraryManager.Infrastructure.Repositories.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManager.Infrastructure.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository repository;

        public TeacherService(ITeacherRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<Teacher>> GetAll()
        {
            var teachers = await this.repository.GetAll();
            return teachers;
        }

        public async Task<bool> Create(CreateTeacherDto teacher)
        {
            var newTeacher = new Teacher
            {
                Name = teacher.Name,
                Email = teacher.Email
            };

            this.repository.Insert(newTeacher);
            var result = await this.repository.Save();

            return result;
        }
    }
}
