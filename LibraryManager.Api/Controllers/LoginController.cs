using LibraryManager.Api.Configurations;
using LibraryManager.Domain.Abstractions.Services;
using LibraryManager.Domain.Dtos;
using LibraryManager.Domain.Entities;
using LibraryManager.Infrastructure.Repositories.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Cors;

namespace LibraryManager.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ITeacherRepository repository;
        private readonly ITokenService tokenService;
        private readonly IAuthService authService;
        private readonly IEncryptService encryptService;
        private readonly ILogger<LoginController> logger;

        public LoginController(
            ITeacherRepository repository,
            ITokenService tokenService,
            IAuthService authService,
            IEncryptService encryptService,
            ILogger<LoginController> logger)
        {
            this.repository = repository;
            this.tokenService = tokenService;
            this.authService = authService;
            this.encryptService = encryptService;
            this.logger = logger;
        }

        [EnableCors]
        [HttpPost("login")]
        public async Task<ActionResult<dynamic>> AuthenticateAsync([FromBody] LoginDto loginDto)
        {
            var encrytpedPassword = this.encryptService.Encrypt(loginDto.Password);
            var user = await this.repository.GetByEmailAndPassword(loginDto.Email, encrytpedPassword);

            if(user == null)
                return NotFound("Email or password are invalid.");

            var token = this.tokenService.GenerateToken(user);
            
            user.Password = "";

            return Ok(new
            {
                user,
                token
            });
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                registerDto.Password = this.encryptService.Encrypt(registerDto.Password);
                var result = await this.authService.Register(registerDto);
                logger.LogInformation($"The user {registerDto.Name} has been created.");
                return Ok("Registered");
            }
            catch
            {
                return StatusCode(500, "An Error Occured");
            }
        }

        [HttpPatch("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
        {
            try
            {
                var result = await this.authService.ChangePassword(changePasswordDto);

                if (result)
                {
                    return Ok("Change Password");
                }
            }
            catch
            {
                return StatusCode(500, "An error has occurred.");
            }

             return StatusCode(500, "An error has occurred.");
        }
    }
}
