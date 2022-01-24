namespace LibraryManager.Domain.Dtos.Books
{
    public class UpdateBookDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
