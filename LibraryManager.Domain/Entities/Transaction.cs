namespace LibraryManager.Domain.Entities;

public sealed class Transaction : BaseEntity
{
    public bool Active { get; set; } = true;

#nullable enable
    public Book? Book { get; set; }
#nullable disable

    public long? BookId { get; set; }

    public DateTimeOffset LendDate { get; set; }

#nullable enable
    public Student? Student { get; set; }
#nullable disable

    public long? StudentId { get; set; }

    public DateTimeOffset? ReturnedAt { get; set; }

    public DateTimeOffset ReturnDate { get; set; }
}
