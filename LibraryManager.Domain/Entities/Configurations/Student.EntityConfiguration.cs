﻿namespace LibraryManager.Domain.Entities.Configurations;

public sealed class StudentEntityConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.ToTable("Student")
            .HasKey(x => x.Id);

        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Email);
        builder.Property(x => x.Password);

        builder.Property(x => x.CreateDate)
            .IsRequired()
            .HasDefaultValue(DateTimeOffset.UtcNow);
        builder.Property(x => x.IsRemoved)
            .IsRequired()
            .HasDefaultValue(false);

        builder.HasIndex(x => x.Name).IsUnique();

        builder.HasOne(x => x.Teacher)
            .WithMany(x => x.Students)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(x => x.Transactions)
            .WithOne(x => x.Student)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
