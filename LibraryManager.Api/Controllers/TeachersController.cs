using LibraryManager.Domain.Abstractions.Services;
using LibraryManager.Domain.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManager.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TeachersController : ControllerBase
    {
        private readonly ITeacherService service;

        public TeachersController(ITeacherService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var teachers = await this.service.GetAll();
            if (!teachers.Any())
            {
                return NotFound("No teachers have been found.");
            }

            return Ok(teachers);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTeacherDto teacher)
        {
            if(!ModelState.IsValid)
                return BadRequest();

            try
            {
                await this.service.Create(teacher);
                return Ok("Teacher Created.");
            }
            catch(Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpGet("TeacherReport")]
        public async Task<IActionResult> TeacherReport()
        {
            var report = await this.service.GetTeachersWithStudents();

            return Ok(report);
        }
    }
}
