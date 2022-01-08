using System;

namespace LibraryManager.Domain.Dtos.Books
{
    public class LendBookDto
    {
        public long BookId { get; set; }
        public long StudentId { get; set; }
    }
}
