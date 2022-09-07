namespace LibraryManager.Domain.Entities;

public class BaseEntity
{
    public long Id { get; set; }

    public DateTimeOffset CreateDate { get; set; }

    public bool IsRemoved { get; set; }
}
