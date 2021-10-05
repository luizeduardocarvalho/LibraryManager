namespace LibraryManager.Domain.Entities.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using System;

    public sealed class TeacherEntityConfiguration : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.ToTable("Teacher")
                .HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Email).IsRequired();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.CreateDate)
                .IsRequired()
                .HasDefaultValue(DateTimeOffset.UtcNow);
            builder.Property(x => x.IsRemoved)
                .IsRequired()
                .HasDefaultValue(false);
        }
    }
}
