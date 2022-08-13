using LibraryManager.Domain.Abstractions.Services;
using LibraryManager.Domain.Dtos.Students;
using LibraryManager.Domain.Entities;
using LibraryManager.Infrastructure.Repositories.Abstractions;
using System;
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
            try
            {
                var students = await this.repository.GetAll();
                return students;
            }
            catch
            {
                throw new Exception("An error occurred while getting all students.");
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
            catch
            {
                throw new Exception("An error occurred while creating the student.");
            }
        }

        public async Task<IEnumerable<StudentsWithBooksDto>> GetStudentsWithBooksByTeacher(long teacherId)
        {
            try
            {
                return await this.repository.GetStudentsWithBooksByTeacher(teacherId);
            }
            catch
            {
                throw new Exception("An error occurred while gettings the student list.");
            }
        }

        public async Task<IEnumerable<GetStudentsDto>> GetStudentsByName(string name)
        {
            try
            {
                return await this.repository.GetStudentsByName(name);
            }
            catch
            {
                throw new Exception("An error occurred while getting the students by name.");
            }
        }

        public async Task<GetStudentWithTransactionsDto> GetStudentWithTransactionsById(long studentId)
        {
            try
            {
                return await this.repository.GetStudentWithTransactionsById(studentId);
            }
            catch
            {
                throw new Exception("An error occurred while getting the student list.");
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
            catch
            {
                throw new Exception("An error occurred while updating the student's teacher.");
            }
        }
    }
}
