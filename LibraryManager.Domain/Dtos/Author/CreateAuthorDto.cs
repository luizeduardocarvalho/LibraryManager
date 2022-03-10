using System.ComponentModel.DataAnnotations;

namespace LibraryManager.Domain.Dtos.Author
{
    public class CreateAuthorDto
    {
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Name { get; set; }
    }
}
