using LibraryManager.Domain.Dtos;
using LibraryManager.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManager.Domain.Abstractions.Services
{
    public interface ITeacherService
    {
        Task<IEnumerable<Teacher>> GetAll();
        Task<bool> Create(CreateTeacherDto teacher);
    }
}
