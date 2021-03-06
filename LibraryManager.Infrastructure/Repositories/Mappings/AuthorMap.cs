﻿using LibraryManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace LibraryManager.Infrastructure.Repositories.Mappings
{
    public sealed class AuthorMap : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.Property(t => t.Id).HasColumnName("id").IsRequired().HasColumnType("INT").ValueGeneratedOnAdd();
            builder.Property(t => t.Name).HasColumnName("name").IsRequired().HasColumnType("VARCHAR").HasMaxLength(150);

            builder.HasKey(t => t.Id);

            builder.HasMany(a => a.BookList).WithOne(b => b.Author).HasForeignKey(b => b.AuthorId);
        }
    }
}
