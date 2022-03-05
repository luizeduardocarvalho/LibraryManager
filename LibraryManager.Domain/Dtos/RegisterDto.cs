using System.ComponentModel.DataAnnotations;

namespace LibraryManager.Domain.Dtos
{
    public class RegisterDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
        public string Password { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
