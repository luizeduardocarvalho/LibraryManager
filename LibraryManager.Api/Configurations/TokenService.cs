using LibraryManager.Api.Configurations;
using LibraryManager.Domain.Abstractions.Services;
using LibraryManager.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LibraryManager.Api.Services
{
    public class TokenService : ITokenService
    {
        private readonly IOptions<Settings> settings;

        public TokenService(IOptions<Settings> settings)
        {
            this.settings = settings;
        }

        public string GenerateToken(User user)
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
    }
}
