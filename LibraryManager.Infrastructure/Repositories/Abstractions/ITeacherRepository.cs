﻿namespace LibraryManager.Infrastructure.Repositories.Abstractions;

public interface ITeacherRepository : IBaseRepository<Teacher>
{
    new Task<IEnumerable<GetTeacherDto>> GetAll();
    Task<User> GetByEmailAndPassword(string email, string password);
    Task<IEnumerable<GetTeacherWithStudentsDto>> GetTeachersWithStudents();
    Task<int> GetLastReference();
    Task<Teacher> GetByReference(int reference);
    Task<GetTeacherDto> GetTeacherById(long id);
}
