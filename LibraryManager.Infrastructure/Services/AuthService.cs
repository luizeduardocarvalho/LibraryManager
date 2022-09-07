﻿namespace LibraryManager.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly ITeacherRepository repository;
    private readonly IEncryptService service;

    public AuthService(ITeacherRepository repository, IEncryptService service)
    {
        this.repository = repository;
        this.service = service;
    }

    public async Task<bool> Register(RegisterDto registerDto)
    {
        var reference = await this.repository.GetLastReference() + 1;

        var user = new Teacher
        {
            Email = registerDto.Email,
            Name = registerDto.Name,
            Password = registerDto.Password,
            Role = registerDto.Role,
            Reference = reference
        };

        try
        {
            this.repository.Insert(user);
            return await this.repository.Save();
        }
        catch
        {
            throw;
        }
    }

    public async Task<bool> ChangePassword(ChangePasswordDto changePasswordDto)
    {
        var oldEncryptedPassword = this.service.Encrypt(changePasswordDto.OldPassword);
        var user = await this.repository.GetByEmailAndPassword(changePasswordDto.Email, oldEncryptedPassword);

        if (user != null)
        {
            try
            {
                user.Password = this.service.Encrypt(changePasswordDto.NewPassword);
                return await this.repository.Save();
            }
            catch
            {
                throw;
            }
        }

        return false;
    }
}
