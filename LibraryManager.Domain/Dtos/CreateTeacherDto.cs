using System.ComponentModel.DataAnnotations;

namespace LibraryManager.Domain.Dtos
{
    public class CreateTeacherDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
