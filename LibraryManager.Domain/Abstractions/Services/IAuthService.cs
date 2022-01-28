using LibraryManager.Domain.Dtos;
using System.Threading.Tasks;

namespace LibraryManager.Domain.Abstractions.Services
{
    public interface IAuthService
    {
        Task<bool> Register(RegisterDto registerDto);
        Task<bool> ChangePassword(ChangePasswordDto changePasswordDto);
    }
}
