﻿namespace LibraryManager.Domain.Entities;

public sealed class Book : BaseEntity
{
    public int Reference { get; set; }

    public Author Author { get; set; }

    public long AuthorId { get; set; }

    public int CheckoutPeriod { get; set; }

    public string Description { get; set; }

    public string Title { get; set; }

    public IList<Transaction> Transactions { get; set; }
}
