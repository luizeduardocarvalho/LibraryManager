﻿using LibraryManager.Domain.Abstractions.Services;
using LibraryManager.Domain.Dtos;
using LibraryManager.Domain.Entities;
using LibraryManager.Infrastructure.Repositories.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LibraryManager.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ITeacherRepository repository;
        private readonly ITokenService tokenService;

        public LoginController(
            ITeacherRepository repository,
            ITokenService tokenService)
        {
            this.repository = repository;
            this.tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<dynamic>> AuthenticateAsync([FromBody] LoginDto loginDto)
        {
            var user = await this.repository.GetByEmailAndPassword(loginDto.Email, loginDto.Password);

            if(user == null)
                return NotFound("Email or password are invalid.");

            var token = this.tokenService.GenerateToken(user);
            
            user.Password = "";

            return new
            {
                user,
                token
            };
        }

        [HttpGet("anonymous")]
        [AllowAnonymous]
        public string Anonymous() => "Anonymous";

        [HttpGet("authorized")]
        [Authorize]
        public string Authorized() => $"Authenticated - {User.Identity.Name}";

        [HttpGet("administrator")]
        [Authorize(Roles = "Administrator")]
        public string AuthorizedAdministrator() => $"Administrator";
    }
}