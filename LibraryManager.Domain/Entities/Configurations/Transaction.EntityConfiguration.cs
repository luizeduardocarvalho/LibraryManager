namespace LibraryManager.Domain.Entities.Configurations;

public sealed class TransactionEntityConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("Transaction")
            .HasKey(x => x.Id);

        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.BookId);
        builder.Property(x => x.StudentId);
        builder.Property(x => x.CreateDate)
            .IsRequired()
            .HasDefaultValue(DateTimeOffset.UtcNow);
        builder.Property(x => x.LendDate);
        builder.Property(x => x.IsRemoved)
            .IsRequired()
            .HasDefaultValue(false);

        builder.HasOne(x => x.Student)
            .WithMany(x => x.Transactions)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(x => x.Book)
            .WithMany(x => x.Transactions)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
