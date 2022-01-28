namespace LibraryManager.Domain.Abstractions.Services
{
    public interface IEncryptService
    {
        string Encrypt(string password);
    }
}
