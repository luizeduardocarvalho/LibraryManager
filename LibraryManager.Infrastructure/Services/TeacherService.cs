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

        public async Task<IEnumerable<GetTeacherDto>> GetAll()
        {
            try
            {
                return await this.repository.GetAll();
            }
            catch
            {
                throw new Exception("An error occurred while getting all teachers.");
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
            catch
            {
                throw new Exception("An error occurred while creating the teacher.");
            }
        }

        public async Task<Teacher> GetByEmailAndPassword(string email, string password)
        {
            try
            {
                var teacher = await this.repository.GetByEmailAndPassword(email, password);

                return teacher;
            }
            catch
            {
                throw new Exception("An error occurred while getting the teacher's email and password.");
            }
        }

        public async Task<IEnumerable<GetTeacherWithStudentsDto>> GetTeachersWithStudents()
        {
            try
            {
                return await this.repository.GetTeachersWithStudents();
            }
            catch
            {
                throw new Exception("An error occurred while getting the teacher list.");
            }
        }

        public async Task<bool> Delete(long id)
        {
            try
            {
                var teacher = await this.repository.GetEntityById(id);

                if (teacher != null)
                {
                    return await this.repository.Delete(teacher);
                }

                return false;
            }
            catch
            {
                throw new Exception("An error occurred while deleting the teacher.");
            }
        }

        public async Task<bool> UpdateTeacher(UpdateTeacherDto updateTeacher)
        {
            try
            {
                var teacher = await this.repository.GetEntityById(updateTeacher.Id);

                if (teacher is not null)
                {
                    teacher.Email = updateTeacher.Email;
                    teacher.Name = updateTeacher.Name;
                    teacher.Role = updateTeacher.Role;

                    return await this.repository.Save();
                }

                return false;
            }
            catch
            {
                throw new Exception("An error occurred while updating the teacher.");
            }
        }

        public async Task<GetTeacherDto> GetById(long id)
        {
            try
            {
                return await this.repository.GetById(id);
            }
            catch
            {
                throw new Exception("An error occurred while getting the teacher.");
            }
        }
    }
}
