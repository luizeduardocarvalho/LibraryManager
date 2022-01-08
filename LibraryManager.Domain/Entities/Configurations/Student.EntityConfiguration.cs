namespace LibraryManager.Domain.Entities.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using System;

    public sealed class StudentEntityConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Student")
                .HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name);
            builder.Property(x => x.Email);
            builder.Property(x => x.Password);

            builder.Property(x => x.CreateDate)
                .IsRequired()
                .HasDefaultValue(DateTimeOffset.UtcNow);
            builder.Property(x => x.IsRemoved)
                .IsRequired()
                .HasDefaultValue(false);

            builder.HasOne(x => x.Teacher)
                .WithMany(x => x.Students);
        }
    }
}
