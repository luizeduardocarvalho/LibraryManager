namespace LibraryManager.Domain.Abstractions.Services;

public interface ITokenService
{
    string GenerateToken(User user);
}
