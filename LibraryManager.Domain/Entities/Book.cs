namespace LibraryManager.Domain.Entities
{
    using System.Collections.Generic;

    public sealed class Book : BaseEntity
    {
        public long AuthorId { get; set; }

        public int CheckoutPeriod { get; set; }

        public string Title { get; set; }

        public IList<Transaction> Transactions { get; set; }
    }
}
