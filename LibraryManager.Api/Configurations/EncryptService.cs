namespace LibraryManager.Api.Configurations;

public class EncryptService : IEncryptService
{
    private readonly IOptions<Settings> settings;
    private readonly ILogger<EncryptService> logger;

    public EncryptService(IOptions<Settings> settings, ILogger<EncryptService> logger)
    {
        this.settings = settings;
        this.logger = logger;
    }

    public string Encrypt(string password)
    {
        var key = this.settings.Value.Secret;
        if (string.IsNullOrEmpty(key))
        {
            key = Environment.GetEnvironmentVariable("Settings");
        }

        var keyBytes = Encoding.UTF8.GetBytes(key);

        var passwordBytes = Encoding.UTF8.GetBytes(password);
        var hash = new System.Security.Cryptography.HMACSHA256()
        {
            Key = keyBytes
        };

        var hashedPassword = hash.ComputeHash(passwordBytes);

        return Convert.ToBase64String(hashedPassword);
    }
}
