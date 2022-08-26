namespace LibraryManager.Domain.Entities;

public sealed class Teacher : User
{
    public string Name { get; set; }

    public int Reference { get; set; }

    public IList<Student> Students { get; set; }
}