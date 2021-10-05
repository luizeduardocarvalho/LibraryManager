namespace LibraryManager.Domain.Entities
{
    using System.Collections.Generic;

    public sealed class Author : BaseEntity
    {
        public IList<Book> Books { get; set; }

        public string Name { get; set; }
    }
}
