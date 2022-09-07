namespace LibraryManager.Domain.Dtos.Teacher;

public class UpdateTeacherDto
{
    [Required]
    public long Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string Role { get; set; }
}
