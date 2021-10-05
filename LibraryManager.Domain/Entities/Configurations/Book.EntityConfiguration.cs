using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace LibraryManager.Domain.Entities.Configurations
{
    public sealed class BookEntityConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Book")
                .HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Title).IsRequired();
            builder.Property(x => x.AuthorId);
            builder.Property(x => x.CreateDate)
                .IsRequired()
                .HasDefaultValue(DateTimeOffset.UtcNow);
            builder.Property(x => x.IsRemoved)
                .IsRequired()
                .HasDefaultValue(false);
        }
    }
}
