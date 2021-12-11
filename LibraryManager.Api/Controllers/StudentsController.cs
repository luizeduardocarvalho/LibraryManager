using LibraryManager.Domain.Abstractions.Services;
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

            if(!students.Any())
            {
                return NotFound("No students have been found.");
            }

            return Ok(students);
        }
    }
}
