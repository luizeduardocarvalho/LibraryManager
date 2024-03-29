﻿namespace LibraryManager.Api.Controllers;

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
            return Ok();
        }

        return BadRequest("An error occurred while creating the student.");
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
        if (name == null)
        {
            return BadRequest();
        }

        var students = await this.service.GetStudentsByName(name);

        return Ok(students);
    }

    [HttpGet("GetStudentWithTransactionsById")]
    public async Task<IActionResult> GetStudentWithTransactionsById([FromQuery] long studentId)
    {
        if (studentId == 0)
        {
            return BadRequest();
        }

        var student = await this.service.GetStudentWithTransactionsById(studentId);

        return Ok(student);
    }

    [HttpPatch("UpdateStudentTeacher")]
    public async Task<IActionResult> UpdateStudentTeacher([FromBody] UpdateStudentTeacherDto updateStudentDto)
    {
        if (updateStudentDto == null)
        {
            return BadRequest();
        }

        var result = await this.service.UpdateStudentTeacher(updateStudentDto);

        if (result)
        {
            return Ok();
        }

        return StatusCode(500, "Error");
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] long id)
    {
        await this.service.Delete(id);

        return Ok();
    }
}
