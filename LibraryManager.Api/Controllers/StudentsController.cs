using LibraryManager.Domain.Abstractions.Services;
using LibraryManager.Domain.Dtos.Students;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManager.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService service;

        public StudentsController(IStudentService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var students = await this.service.GetAll();

            if (!students.Any())
            {
                return NotFound("No students have been found.");
            }

            return Ok(students);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStudentDto createStudentDto)
        {
            if (createStudentDto == null)
            {
                return BadRequest();
            }

            var result = await this.service.Create(createStudentDto);

            if (result)
            {
                return Ok("Student created");
            }

            return StatusCode(500, "Unexpected error");
        }

        [HttpGet("StudentsWithBooks")]
        public async Task<IActionResult> GetStudentsWithBookCountByTeacher([FromQuery] GetStudentByTeacherDto getStudentByTeacherDto)
        {
            if (getStudentByTeacherDto == null)
            {
                return BadRequest();
            }

            var students = await this.service.GetStudentsWithBooksByTeacher(getStudentByTeacherDto.TeacherId);

            return Ok(students);
        }

        [HttpGet("GetStudentsByName")]
        public async Task<IActionResult> GetStudentsByName([FromQuery] string name)
        {
            if(name == null)
            {
                return BadRequest();
            }

            var students = await this.service.GetStudentsByName(name);

            return Ok(students);
        }
    }
}
