namespace LibraryManager.Domain.Entities
{
    using System;

    public sealed class Transaction : BaseEntity
    {
        public bool Active { get; set; }

        public long BookId { get; set; }

        public DateTimeOffset CheckoutDate { get; set; }

        public long StudentId { get; set; }

        public DateTimeOffset ReturnedAt { get; set; }

        public DateTimeOffset ReturnDate { get; set; }
    }
}
