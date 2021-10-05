namespace LibraryManager.Domain.Entities
{
    public sealed class Book : BaseEntity
    {
        public string Title { get; set; }

        public string Author { get; set; }
    }
}
