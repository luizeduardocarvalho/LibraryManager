﻿namespace LibraryManager.Domain.Entities.Configurations;

public sealed class BookEntityConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.ToTable("Book")
            .HasKey(x => x.Id);

        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Title).IsRequired().HasMaxLength(150);
        builder.Property(x => x.AuthorId);
        builder.Property(x => x.CreateDate)
            .IsRequired()
            .HasDefaultValue(DateTimeOffset.UtcNow);
        builder.Property(x => x.Reference);
        builder.Property(x => x.IsRemoved)
            .IsRequired()
            .HasDefaultValue(false);

        builder.HasOne(x => x.Author)
            .WithMany(x => x.Books);

        builder.HasMany(x => x.Transactions)
            .WithOne(x => x.Book)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
