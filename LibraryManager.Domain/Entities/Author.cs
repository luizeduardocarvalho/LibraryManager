namespace LibraryManager.Domain.Entities;

public sealed class Author : BaseEntity
{
    public IList<Book> Books { get; set; }

    public string Name { get; set; }
}
