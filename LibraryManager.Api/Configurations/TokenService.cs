namespace LibraryManager.Api.Services;

public class TokenService : ITokenService
{
    private readonly IOptions<Settings> settings;
    private readonly ILogger<TokenService> logger;

    public TokenService(
        IOptions<Settings> settings,
        ILogger<TokenService> logger)
    {
        this.settings = settings;
        this.logger = logger;
    }

    public string GenerateToken(User user)
    {
        try
        {
            var secret = this.settings.Value.Secret;
            if (string.IsNullOrEmpty(secret))
                secret = Environment.GetEnvironmentVariable("Settings");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while generating the token. {0}", e.Message);
            throw;
        }
    }
}
