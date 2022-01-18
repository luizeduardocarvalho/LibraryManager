﻿// <auto-generated />
using System;
using LibraryManager.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace LibraryManager.Infrastructure.Migrations
{
    [DbContext(typeof(LibraryManagerDbContext))]
    [Migration("20220108010142_AddPasswordToStudent")]
    partial class AddPasswordToStudent
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("LibraryManager.Domain.Entities.Author", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTimeOffset>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValue(new DateTimeOffset(new DateTime(2022, 1, 8, 1, 1, 42, 602, DateTimeKind.Unspecified).AddTicks(647), new TimeSpan(0, 0, 0, 0, 0)));

                    b.Property<bool>("IsRemoved")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Author");
                });

            modelBuilder.Entity("LibraryManager.Domain.Entities.Book", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long>("AuthorId")
                        .HasColumnType("bigint");

                    b.Property<int>("CheckoutPeriod")
                        .HasColumnType("integer");

                    b.Property<DateTimeOffset>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValue(new DateTimeOffset(new DateTime(2022, 1, 8, 1, 1, 42, 614, DateTimeKind.Unspecified).AddTicks(2187), new TimeSpan(0, 0, 0, 0, 0)));

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool>("IsRemoved")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<long?>("StudentId")
                        .HasColumnType("bigint");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("StudentId");

                    b.ToTable("Book");
                });

            modelBuilder.Entity("LibraryManager.Domain.Entities.Student", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTimeOffset>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValue(new DateTimeOffset(new DateTime(2022, 1, 8, 1, 1, 42, 616, DateTimeKind.Unspecified).AddTicks(2393), new TimeSpan(0, 0, 0, 0, 0)));

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<bool>("IsRemoved")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("Role")
                        .HasColumnType("text");

                    b.Property<long>("TeacherId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("TeacherId");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("LibraryManager.Domain.Entities.Teacher", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTimeOffset>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValue(new DateTimeOffset(new DateTime(2022, 1, 8, 1, 1, 42, 617, DateTimeKind.Unspecified).AddTicks(4258), new TimeSpan(0, 0, 0, 0, 0)));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsRemoved")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Teacher");
                });

            modelBuilder.Entity("LibraryManager.Domain.Entities.Transaction", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("Active")
                        .HasColumnType("boolean");

                    b.Property<long>("BookId")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset>("CheckoutDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValue(new DateTimeOffset(new DateTime(2022, 1, 8, 1, 1, 42, 618, DateTimeKind.Unspecified).AddTicks(4919), new TimeSpan(0, 0, 0, 0, 0)));

                    b.Property<bool>("IsRemoved")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<DateTimeOffset>("ReturnDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset?>("ReturnedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("StudentId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("StudentId");

                    b.ToTable("Transaction");
                });

            modelBuilder.Entity("LibraryManager.Domain.Entities.Book", b =>
                {
                    b.HasOne("LibraryManager.Domain.Entities.Author", "Author")
                        .WithMany("Books")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LibraryManager.Domain.Entities.Student", null)
                        .WithMany("Books")
                        .HasForeignKey("StudentId");

                    b.Navigation("Author");
                });

            modelBuilder.Entity("LibraryManager.Domain.Entities.Student", b =>
                {
                    b.HasOne("LibraryManager.Domain.Entities.Teacher", "Teacher")
                        .WithMany("Students")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("LibraryManager.Domain.Entities.Transaction", b =>
                {
                    b.HasOne("LibraryManager.Domain.Entities.Book", "Book")
                        .WithMany("Transactions")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LibraryManager.Domain.Entities.Student", "Student")
                        .WithMany("Transactions")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("LibraryManager.Domain.Entities.Author", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("LibraryManager.Domain.Entities.Book", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("LibraryManager.Domain.Entities.Student", b =>
                {
                    b.Navigation("Books");

                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("LibraryManager.Domain.Entities.Teacher", b =>
                {
                    b.Navigation("Students");
                });
#pragma warning restore 612, 618
        }
    }
}