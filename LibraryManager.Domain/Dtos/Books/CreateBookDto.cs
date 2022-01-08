using System.ComponentModel.DataAnnotations;

namespace LibraryManager.Domain.Dtos
{
    public class CreateBookDto
    {
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public int AuthorId { get; set; }
    }
}
