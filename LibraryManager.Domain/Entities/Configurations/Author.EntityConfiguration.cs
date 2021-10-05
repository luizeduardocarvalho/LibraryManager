using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace LibraryManager.Domain.Entities.Configurations
{
    public sealed class AuthorEntityConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.ToTable("Author")
                .HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.CreateDate)
                .IsRequired()
                .HasDefaultValue(DateTimeOffset.UtcNow);
            builder.Property(x => x.IsRemoved)
                .IsRequired()
                .HasDefaultValue(false);

            builder.HasMany(x => x.Books)
                .WithOne(x => x.Author);
        }
    }
}
