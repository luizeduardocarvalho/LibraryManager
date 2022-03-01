namespace LibraryManager.Domain.Entities
{
    using System.Collections.Generic;

    public sealed class Teacher : User
    {
        public string Name { get; set; }

        public int Reference { get; set; }

        public IList<Student> Students { get; set; }
    }
}
