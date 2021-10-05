namespace LibraryManager.Domain.Entities
{
    using System.Collections.Generic;

    public sealed class Student : BaseEntity
    {
        public IList<Book> Books { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public Teacher Teacher { get; set; }

        public long TeacherId { get; set; }

        public IList<Transaction> Transactions { get; set; }
    }
}
