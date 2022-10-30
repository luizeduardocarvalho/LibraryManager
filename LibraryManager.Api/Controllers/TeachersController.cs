namespace LibraryManager.Api.Controllers;

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
        await this.service.Create(teacher);
        return Ok();
    }

    [HttpGet("TeacherReport")]
    public async Task<IActionResult> TeacherReport()
    {
        var report = await this.service.GetTeachersWithStudents();

        return Ok(report);
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete([FromQuery] long id)
    {
        var result = await this.service.Delete(id);

        if (result)
        {
            return Ok();
        }

        return BadRequest("Error");
    }

    [HttpPatch("Update")]
    public async Task<IActionResult> Update([FromBody] UpdateTeacherDto updateTeacher)
    {
        var result = await this.service.UpdateTeacher(updateTeacher);

        if (result)
        {
            return Ok("Success");
        }

        return StatusCode(500, "Error");
    }

    [HttpGet("GetById")]
    public async Task<IActionResult> GetById([FromQuery] long id)
    {
        if (id == 0)
        {
            return BadRequest();
        }

        var teacher = await this.service.GetById(id);

        if (teacher is not null)
        {
            return Ok(teacher);
        }

        return StatusCode(500, "Error");
    }
}
