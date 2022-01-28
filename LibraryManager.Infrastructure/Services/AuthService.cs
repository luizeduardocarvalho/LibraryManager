﻿using LibraryManager.Domain.Abstractions.Services;
using LibraryManager.Domain.Dtos;
using LibraryManager.Domain.Entities;
using LibraryManager.Infrastructure.Repositories.Abstractions;
using System.Threading.Tasks;

namespace LibraryManager.Infrastructure.Services
{
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
            var user = new Teacher
            {
                Email = registerDto.Email,
                Name = registerDto.Name,
                Password = registerDto.Password,
                Role = registerDto.Role
            };

            this.repository.Insert(user);
            var result = await this.repository.Save();

            return result;
        }

        public async Task<bool> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            var oldEncryptedPassword = this.service.Encrypt(changePasswordDto.OldPassword);
            var user = await this.repository.GetByEmailAndPassword(changePasswordDto.Email, oldEncryptedPassword);
            var result = false;

            if(user != null)
            {
                user.Password = this.service.Encrypt(changePasswordDto.NewPassword);
                result = await this.repository.Save();
            }

            return result;
        }
    }
}