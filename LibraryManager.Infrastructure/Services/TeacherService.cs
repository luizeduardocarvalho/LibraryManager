using LibraryManager.Domain.Abstractions.Services;
using LibraryManager.Domain.Dtos;
using LibraryManager.Domain.Dtos.Teacher;
using LibraryManager.Domain.Entities;
using LibraryManager.Infrastructure.Repositories.Abstractions;
using System;
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
                Email = teacher.Email,
                Password = teacher.Password,
                Role = teacher.Role
            };

            this.repository.Insert(newTeacher);
            var result = await this.repository.Save();

            return result;
        }

        public async Task<Teacher> GetByEmailAndPassword(string email, string password)
        {
            var teacher = await this.repository.GetByEmailAndPassword(email, password);

            return teacher;
        }

        public async Task<IEnumerable<GetTeacherWithStudentsDto>> GetTeachersWithStudents()
        {
            return await this.repository.GetTeachersWithStudents();
        }

        public async Task<bool> Delete(long id)
        {
            var result = false;
            try
            {
                var teacher = await this.repository.GetEntityById(id);

                if(teacher != null)
                {
                    result = await this.repository.Delete(teacher);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }

            return result;
        }
    }
}
