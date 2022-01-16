namespace LibraryManager.Domain.Dtos.Books
{
    public class GetBooksDto
    {
        public string AuthorName { get; set; }
        public long BookId { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
    }
}
