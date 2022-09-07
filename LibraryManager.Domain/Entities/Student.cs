namespace LibraryManager.Domain.Entities;

public sealed class Student : User
{
    public IList<Book> Books { get; set; }

    public string Name { get; set; }

#nullable enable
    public Teacher? Teacher { get; set; }
#nullable disable

    public long? TeacherId { get; set; }

    public IList<Transaction> Transactions { get; set; }
}
