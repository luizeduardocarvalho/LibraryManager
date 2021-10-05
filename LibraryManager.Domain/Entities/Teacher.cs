namespace LibraryManager.Domain.Entities
{
    using System.Collections.Generic;

    public sealed class Teacher : BaseEntity
    {
        public string Email { get; set; }

        public string Name { get; set; }

        public IList<Student> Students { get; set; }
    }
}
